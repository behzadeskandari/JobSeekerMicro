using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace AppJob.Core.FileStorage
{
    public class AzureBlobStorageService : IFileStorageService
    {
        private readonly AzureBlobStorageOptions _options;

        public AzureBlobStorageService(IOptions<FileStorageOptions> fileStorageOptions)
        {
            _options = fileStorageOptions.Value.AzureBlob ?? throw new ArgumentNullException(nameof(fileStorageOptions.Value.AzureBlob));
            if (string.IsNullOrEmpty(_options.ConnectionString))
            {
                throw new ArgumentException("AzureBlob ConnectionString is not configured.");
            }
        }

        private BlobContainerClient GetBlobContainerClient(string? containerName = null)
        {
            var container = string.IsNullOrEmpty(containerName) ? _options.DefaultContainerName : containerName;
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentException("Blob container name is not specified.");
            }
            var blobServiceClient = new BlobServiceClient(_options.ConnectionString);
            return blobServiceClient.GetBlobContainerClient(container);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string? containerName = null)
        {
            var containerClient = GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true); // Overwrite if exists
            return blobClient.Uri.ToString(); // Return the full URI of the blob
        }

        public async Task<Stream?> DownloadFileAsync(string filePath, string? containerName = null)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(filePath);
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadStreamingAsync();
                return response.Value.Content;
            }
            return null;
        }

        public async Task<bool> DeleteFileAsync(string filePath, string? containerName = null)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(filePath);
            var response = await blobClient.DeleteIfExistsAsync();
            return response.Value;
        }

        public async Task<bool> FileExistsAsync(string filePath, string? containerName = null)
        {
            var containerClient = GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(filePath);
            return await blobClient.ExistsAsync();
        }
    }
}
