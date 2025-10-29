using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string? containerName = null);
        Task<Stream?> DownloadFileAsync(string filePath, string? containerName = null);
        Task<bool> DeleteFileAsync(string filePath, string? containerName = null);
        Task<bool> FileExistsAsync(string filePath, string? containerName = null);
        // Add other file management operations as needed (e.g., list files)
    }
}
