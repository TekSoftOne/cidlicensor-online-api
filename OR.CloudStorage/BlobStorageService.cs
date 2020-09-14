using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OR.CloudStorage.Configurations;

namespace OR.CloudStorage
{
    public interface IBlobStorageService
    { 
        Task<CloudBlobContainer> GetBlobStorageClientAsync(string containerName);
        Task<IEnumerable<string>> GetDataOfSubFolder(string containerName, string folder, string subFolder);
        Task DeleteBlobItems(string containerName, string folder);

        string GetRootDocuments();
    }
    public class BlobStorageService: IBlobStorageService
    {
        private readonly IOptions<AzureBlobOptions> _blobOptions;
        public BlobStorageService(IOptions<AzureBlobOptions> blobOptions)
        {
            _blobOptions = blobOptions;
        }
        public async Task<CloudBlobContainer> GetBlobStorageClientAsync(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(_blobOptions.Value.BlobConnection);

            var blobClient = storageAccount.CreateCloudBlobClient();       

            var container = blobClient.GetContainerReference(containerName);

            if (await container.CreateIfNotExistsAsync())
            {
                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return container;
        }

        public async Task DeleteBlobItems(string containerName, string folder)
        {
            var container = await GetBlobStorageClientAsync(containerName);
            foreach (IListBlobItem blob in container.GetDirectoryReference($"{_blobOptions.Value.RootDocuments}/{folder}").ListBlobs(true))
            {
                if (blob is CloudBlob)
                {
                    var item = blob as CloudBlob;
                    await item.DeleteIfExistsAsync();
                }
            }
        }

        public async Task<IEnumerable<string>> GetDataOfSubFolder(string containerName, string folder, string subFolder)
        {
            var container = await GetBlobStorageClientAsync(containerName);
            BlobContinuationToken continuationToken = null;

            var subFolderBlob = container.GetDirectoryReference($"{this._blobOptions.Value.RootDocuments}/{folder}/{subFolder}");
            var results = new List<IListBlobItem>();
            do
            {
                var response = await subFolderBlob.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                results.AddRange(response.Results);
            }
            while (continuationToken != null);
            return results.Select(x => x.Uri.AbsoluteUri);
        }

        public string GetRootDocuments()
        {
            return this._blobOptions.Value.RootDocuments;
        }
    }
}
