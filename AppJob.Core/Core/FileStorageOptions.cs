using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core.Enums;

namespace AppJob.Core.Core
{
    public class FileStorageOptions
    {
        
        public StorageLocation DefaultStorageLocation { get; set; }
        public LocalDiskStorageOptions? Local { get; set; }
        public AzureBlobStorageOptions? AzureBlob { get; set; }
    }

    public class LocalDiskStorageOptions
    {
        public string? RootPath { get; set; }
    }

    public class AzureBlobStorageOptions
    {
        public string? ConnectionString { get; set; }
        public string? DefaultContainerName { get; set; }
    }
}
