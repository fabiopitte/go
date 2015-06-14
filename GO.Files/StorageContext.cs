using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace GO.Files
{
    public class StorageContext
    {
        private CloudStorageAccount _storageAccount;

        public StorageContext()
        {
            _storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.ConnectionStrings["Azure"].ConnectionString);
        }

        public CloudBlobClient BlobClient
        {
            get { return _storageAccount.CreateCloudBlobClient(); }
        }

        public CloudTableClient TableClient
        {
            get { return _storageAccount.CreateCloudTableClient(); }
        }
    }
}
