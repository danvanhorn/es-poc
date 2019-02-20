using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.Management.Storage.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.IO;

namespace es_poc.Dal.Azure
{
    public class AzureClient : IDisposable
    {
        private static string mStorageAcctName = "devstoreaccount1";
        private static string mStorageAcctKey = "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==";
        private static string mContainerName = "devstoreaccount1";
        private CloudBlobClient mClient;
        private static CloudBlobContainer mContainer;
        private CloudStorageAccount mAcct;
        private string mStorageConnectionString = String.Format(@"DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net;BlobEndpoint=http://azurite:10000/devstoreaccount1;", 
            mStorageAcctName,
            mStorageAcctKey);

        public AzureClient ()
        {
            if(CloudStorageAccount.TryParse(mStorageConnectionString, out mAcct))
            {
                mClient = mAcct.CreateCloudBlobClient();
            }
        }

        public async Task Init() {
            try {

                CloudBlobClient mClient = mAcct.CreateCloudBlobClient();
                mContainer = mClient.GetContainerReference(mContainerName);

                mContainer = mClient.GetContainerReference(mContainerName);
                await mContainer.CreateIfNotExistsAsync();
                Console.WriteLine("Created container '{0}'", mContainer.Name);

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await mContainer.SetPermissionsAsync(permissions);
                uploadImages();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error returned from the service: {0}", e.Message);
            }
        }

        public void uploadImages()
        {
            string[] imagePaths = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Images"));

            foreach (string imagePath in imagePaths)
            {
                try
                {
                    CloudBlockBlob blob = mContainer.GetBlockBlobReference(Path.GetFileName(imagePath));
                    using (Stream file = File.OpenRead(imagePath))
                    {
                        blob.UploadFromStream(file);
                        Console.WriteLine("inserted {0}", Path.GetFileName(imagePath));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error returned from the service: {0}", e.Message);
                }
            }
        }

        public Stream getBlobData(string filename)
        {
            CloudBlockBlob blob = mContainer.GetBlockBlobReference(filename);
            Stream stream = null;
            blob.DownloadToStream(stream);
            return stream;
        }

        void IDisposable.Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}
