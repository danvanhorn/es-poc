using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.Management.Storage.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace es_poc.Dal.Azure
{
    public class AzureClient
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
                mContainer = mClient.GetContainerReference(mContainerName);
                if (await mContainer.CreateIfNotExistsAsync()) {
                    Console.WriteLine("Created container '{0}'", mContainer.Name);

                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await mContainer.SetPermissionsAsync(permissions);

                    CloudBlockBlob blob = mContainer.GetBlockBlobReference("helloworld.txt");
                    await blob.UploadTextAsync("Hello, World!");
                };
            } catch (Exception e)
            {
                Console.WriteLine("Error returned from the service: {0}", e.Message);
            }
        }

        public void getText(IAsyncResult result)
        {
            // string str = (string) result.;
            Console.WriteLine("In the callback result");
        }

        public void getBlobData()
        {
            CloudBlockBlob blob = mContainer.GetBlockBlobReference("helloworld.txt");
            AsyncCallback callback = new AsyncCallback(getText);
            string foo = blob.DownloadText();
            blob.BeginDownloadText(callback, new object());
        }
    }
}
