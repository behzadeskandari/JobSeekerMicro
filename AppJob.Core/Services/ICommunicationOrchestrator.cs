using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core.Enums;
using AppJob.Core.Core;
using AppJob.Core.Sms;
using Kavenegar.Core.Models;

namespace AppJob.Core.Services
{
    public interface ICommunicationOrchestrator
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, StorageLocation? preferredLocation = null, string? containerName = null);
        Task<Stream?> DownloadFileAsync(string filePath, StorageLocation? location = null, string? containerName = null);
        Task<bool> DeleteFileAsync(string filePath, StorageLocation? location = null, string? containerName = null);
        Task<bool> FileExistsAsync(string filePath, StorageLocation? location = null, string? containerName = null);

        Task<bool> SendEmailAsync(EmailMessage message);
        Task<bool> SendEmailAsync(string to, string subject, string body, System.Collections.Generic.List<string>? attachments = null);
        Task<bool> SendBulkEmailAsync(System.Collections.Generic.List<EmailMessage> messages);

        Task<string> SendSms(SmsRequest smsRequest);

        Task<string> SendSmsBulkArray(SmsRequestBulk smsRequest);
        Task<List<SendResult>> SendBulkSms(SmsRequests smsRequest);

        // Methods for scheduled emails would typically be managed by a separate service
        // or integrated with a background processing library.
    }
}
