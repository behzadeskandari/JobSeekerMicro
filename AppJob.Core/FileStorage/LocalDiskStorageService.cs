using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core;
using Microsoft.Extensions.Options;

namespace AppJob.Core.FileStorage
{
    public class LocalDiskStorageService : IFileStorageService
    {
        private readonly LocalDiskStorageOptions _options;

        
        public LocalDiskStorageService(IOptions<FileStorageOptions> fileStorageOptions)
        {
            _options = fileStorageOptions.Value.Local ?? throw new ArgumentNullException(nameof(fileStorageOptions.Value.Local));
            if (string.IsNullOrEmpty(_options.RootPath))
            {
                throw new ArgumentException("LocalDisk RootPath is not configured.");
            }
            Directory.CreateDirectory(_options.RootPath); // Ensure root directory exists
        }

        private string GetFullPath(string filePath)
        {
            return Path.Combine(_options.RootPath!, filePath);
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string? containerName = null)
        {
            var fullPath = GetFullPath(fileName);
            var directory = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await using (var fileStreamToWrite = File.Create(fullPath))
            {
                await fileStream.CopyToAsync(fileStreamToWrite);
            }
            return fileName; // Return the relative path
        }

        public async Task<Stream?> DownloadFileAsync(string filePath, string? containerName = null)
        {
            var fullPath = GetFullPath(filePath);
            if (File.Exists(fullPath))
            {
                return File.OpenRead(fullPath);
            }
            return null;
        }

        public async Task<bool> DeleteFileAsync(string filePath, string? containerName = null)
        {
            var fullPath = GetFullPath(filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<bool> FileExistsAsync(string filePath, string? containerName = null)
        {
            return File.Exists(GetFullPath(filePath));
        }
    }
}
