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
        private CloudBlobContainer mContainer;
        private CloudStorageAccount mAcct;
        private string mStorageConnectionString = String.Format(@"DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net;BlobEndpoint=http://azurite:10000/devstoreaccount1;", 
            mStorageAcctName,
            mStorageAcctKey);

        public AzureClient ()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(mStorageConnectionString);
            mClient = account.CreateCloudBlobClient();

            mContainer = mClient.GetContainerReference(mContainerName);
            bool x = mContainer.CreateIfNotExistsAsync().Result;
        }

        public void Init() {

            CloudBlockBlob blob = mContainer.GetBlockBlobReference("helloworld.txt");
            blob.UploadTextAsync("Hello, World!").Wait();
        }

        public void getText(IAsyncResult result)
        {
            string str = (string) result.ToString();
        }

        public void getBlobData()
        {
            CloudBlockBlob blob = mContainer.GetBlockBlobReference("helloworld.txt");
            AsyncCallback callback = new AsyncCallback(getText);
            blob.BeginDownloadText(callback, new object());
        }
    }
}
