using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core.Enums;
using AppJob.Core.Core;
using AppJob.Core.Domain.Entities;
using AppJob.Core.Email;
using AppJob.Core.FileStorage;
using AppJob.Core.Repository;
using AppJob.Core.Sms;
using Kavenegar.Core.Models;
using Microsoft.Extensions.Options;

namespace AppJob.Core.Services
{
    public class CommunicationOrchestrator(
        IOptions<FileStorageOptions> storageOptions,
        LocalDiskStorageService localDiskStorageService,
        AzureBlobStorageService azureBlobStorageService,
        SmtpEmailService smtpEmailService,
        SmsService smService,
        LinkService linkService)
        : ICommunicationOrchestrator
    {
        private readonly FileStorageOptions _storageOptions = storageOptions.Value;
        private readonly IFileStorageService _localDiskStorageService = localDiskStorageService ?? throw new ArgumentNullException(nameof(localDiskStorageService));
        private readonly IFileStorageService _azureBlobStorageService = azureBlobStorageService ?? throw new ArgumentNullException(nameof(azureBlobStorageService));
        private readonly IEmailService _smtpEmailService = smtpEmailService ?? throw new ArgumentNullException(nameof(smtpEmailService));
        private readonly ISmsService _smService = smService ?? throw new ArgumentNullException(nameof(smService));
        private readonly ILinkService _linkService = linkService ?? throw new ArgumentNullException(nameof(linkService));

        private IFileStorageService GetStorageService(StorageLocation? location = null)
        {
            var storageLocation = location ?? _storageOptions.DefaultStorageLocation;
            return storageLocation switch
            {
                StorageLocation.LocalDisk => _localDiskStorageService,
                StorageLocation.AzureBlob => _azureBlobStorageService,
                _ => throw new ArgumentException($"Unsupported storage location: {storageLocation}"),
            };
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, StorageLocation? preferredLocation = null, string? containerName = null)
        {
            return await GetStorageService(preferredLocation).UploadFileAsync(fileStream, fileName, containerName);
        }

        public async Task<Stream?> DownloadFileAsync(string filePath, StorageLocation? location = null, string? containerName = null)
        {
            return await GetStorageService(location).DownloadFileAsync(filePath, containerName);
        }

        public async Task<bool> DeleteFileAsync(string filePath, StorageLocation? location = null, string? containerName = null)
        {
            return await GetStorageService(location).DeleteFileAsync(filePath, containerName);
        }

        public async Task<bool> FileExistsAsync(string filePath, StorageLocation? location = null, string? containerName = null)
        {
            return await GetStorageService(location).FileExistsAsync(filePath, containerName);
        }

        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            return await _smtpEmailService.SendEmailAsync(message);
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, System.Collections.Generic.List<string>? attachments = null)
        {
            return await _smtpEmailService.SendEmailAsync(to, subject, body, attachments);
        }

        public async Task<bool> SendBulkEmailAsync(System.Collections.Generic.List<EmailMessage> messages)
        {
            return await _smtpEmailService.SendBulkEmailAsync(messages);
        }

        public async Task<string> SendSms(SmsRequest smsRequest)
        {
            return await _smService.SendSmsAsync(smsRequest.From, smsRequest.To,smsRequest.Message);
        }

        public async Task<string> SendSmsBulkArray(SmsRequestBulk smsRequest)
        {
            return await _smService.SendSmsAsync(smsRequest.From, smsRequest.To, smsRequest.Message);
        }
        public async Task<List<Kavenegar.Core.Models.SendResult>> SendBulkSms(SmsRequests smsRequest)
        {
            return await _smService.SendBulkSmsAsync(smsRequest.From, smsRequest.Tos, smsRequest.Message);
        }
        public async Task<string> GenerateUniqueLinkAsync(string purpose, DateTime expirationDate, string associatedDate)
        {
            // Implement scheduling logic here
            var record = _linkService.GenerateUniqueLinkAsync(purpose, expirationDate, associatedDate).Result;
            return record;
        }
        public async Task<GeneratedLink> GetLinkDetailsAsync(string token)
        {
            var record = _linkService.GetLinkDetailsAsync(token).Result;
            return record;
        }
        public async Task<bool> ValidateLinkAsync(string token)
        {
            var record = _linkService.ValidateLinkAsync(token).Result;
            return record;
        }





    }
}
