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
        private static string mStorageAcctName = "es-poc";
        private static string mStorageAcctKey = "es-poc-key";
        private static string mResourceGroupName = "es-poc-images";
        private CloudBlobClient mClient;
        private CloudStorageAccount mAcct;
        private string mStorageConnectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net", 
            mStorageAcctName,
            mStorageAcctKey);

        public AzureClient ()
        {
            IStorageAccount storage = azure.StorageAccounts.Define(mStorageAcctName)
                .WithRegion(Region.USEast)
                .WithNewResourceGroup(rgName)
                .Create();

        }
    }
}
