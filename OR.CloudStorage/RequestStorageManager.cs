using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;


namespace OR.CloudStorage
{
    public interface IRequestStorageManager
    {
        Task<string> UploadDocument(string membershiId, string fieldName, IFormFile file);
    }

    public class RequestStorageManager : IRequestStorageManager
    {
        private readonly IBlobStorageService _blobStorage;
        private readonly string _containerName = "onlinerequests";

        public RequestStorageManager(IBlobStorageService blobStorage)
        {
            _blobStorage = blobStorage;
        }

        private async Task DeleteDocuments(string requestId, string fieldName)
        {
            var documents = await GetDocuments(requestId, fieldName);
            await _blobStorage.DeleteBlobItems(_containerName, $"{requestId}/{fieldName}");
        }

        private async Task<IEnumerable<string>> GetDocuments(string membershipNumber, string fieldData)
        {
            var result = await _blobStorage.GetDataOfSubFolder(_containerName, membershipNumber, fieldData);
            if (result == null)
                return new List<string>();
            return result;
        }

        public async Task<string> UploadDocument(string requestId, string fieldName, IFormFile file)
        {
            await this.DeleteDocuments(requestId, fieldName);

            string documentPath = string.Empty;

            if (file == null || file.Length == 0)
                throw new Exception("File is empty!");

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                streamReader.BaseStream.Position = 0;

                var fileData = streamReader.BaseStream;

                var container = await _blobStorage.GetBlobStorageClientAsync(_containerName);

                var rootDocuments = _blobStorage.GetRootDocuments();
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference($"{rootDocuments}/{requestId}/{fieldName}/{file.FileName}");
                cloudBlockBlob.Properties.ContentType = "image/jpeg";
                await cloudBlockBlob.UploadFromStreamAsync(fileData);

                documentPath = cloudBlockBlob.Uri.AbsoluteUri;
            }

            return documentPath;

        }
    }
}
