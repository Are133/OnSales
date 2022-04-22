using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Helpers
{
    public class BlobHelper:IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<CloudBlobContainer>GetContainer(string containerName)
        {
            CloudBlobContainer container =  _blobClient.GetContainerReference(containerName);
            return container;
        }

        public async Task<string>UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            string name = file.FileName;
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }

        public async Task<string> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            string name = $"{Guid.NewGuid()}";
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }

        public async Task<string>DeleteBlobAsync(string name, string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            await blockBlob.DeleteIfExistsAsync();
            return name;
        }


    }
}
