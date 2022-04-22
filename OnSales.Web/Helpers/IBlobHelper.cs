using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnSales.Web.Helpers
{
    public interface IBlobHelper
    {
        //Task<string> GetContainer(string containerName);
        Task<CloudBlobContainer> GetContainer(string name);

        Task<string> UploadBlobAsync(IFormFile imageFile, string containerName);

        Task<string> UploadBlobAsync(string name, string containerName);

        Task<string> DeleteBlobAsync(string name, string containerName);
    }
}
