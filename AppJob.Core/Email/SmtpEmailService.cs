using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core;
using AppJob.Core.Data;
using AppJob.Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace AppJob.Core.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpOptions _options;
        private readonly MyServiceDbContext _dbContext;
        public SmtpEmailService(MyServiceDbContext   dbContext,IOptions<EmailOptions> emailOptions)
        {
            _options = emailOptions.Value.Smtp ?? throw new ArgumentNullException(nameof(emailOptions.Value.Smtp));
            if (string.IsNullOrEmpty(_options.Host) || string.IsNullOrEmpty(_options.Username) || string.IsNullOrEmpty(_options.Password))
            {
                throw new ArgumentException("SMTP settings are not fully configured.");
            }
            _dbContext = dbContext;
        }

        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            using var client = new SmtpClient(_options.Host, _options.Port)
            {
                Credentials = new NetworkCredential(_options.Username, _options.Password),
                EnableSsl = _options.UseSsl
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.DefaultFromAddress ?? _options.Username, _options.DefaultFromName),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true // You might want to make this configurable
            };

            foreach (var recipient in message.To)
            {
                mailMessage.To.Add(new MailAddress(recipient.Email, recipient.Name));
            }
            foreach (var recipient in message.Cc)
            {
                mailMessage.CC.Add(new MailAddress(recipient.Email, recipient.Name));
            }
            foreach (var recipient in message.Bcc)
            {
                mailMessage.Bcc.Add(new MailAddress(recipient.Email, recipient.Name));
            }

            if (message.Attachments != null)
            {
                foreach (var attachmentPath in message.Attachments)
                {
                    if (System.IO.File.Exists(attachmentPath))
                    {
                        mailMessage.Attachments.Add(new Attachment(attachmentPath));
                    }
                    // Consider handling file not found exceptions
                }
            }

            try
            {
                await client.SendMailAsync(mailMessage);
                await _dbContext.EmailMessages.AddAsync(message);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                EmailLog mailLog = new EmailLog();


                mailLog.Subject = message.Subject;
                mailLog.ErrorMessage = message.Body;
                mailLog.IsSent = true;
                mailLog.Timestamp = DateTime.Now;
                mailLog.To = message.To.FirstOrDefault().ToString();
                await _dbContext.EmailLogs.AddAsync(mailLog);
                await _dbContext.SaveChangesAsync();

                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, System.Collections.Generic.List<string>? attachments = null)
        {
            var message = new EmailMessage
            {
                To = new System.Collections.Generic.List<EmailRecipient> { new EmailRecipient { Email = to } },
                Subject = subject,
                Body = body,
                Attachments = attachments ?? new System.Collections.Generic.List<string>()
            };
            return await SendEmailAsync(message);
        }

        public async Task<bool> SendBulkEmailAsync(System.Collections.Generic.List<EmailMessage> messages)
        {
            bool allSent = true;
            foreach (var message in messages)
            {
                if (!await SendEmailAsync(message))
                {
                    allSent = false;
                    // Optionally log which emails failed
                }
            }
            return allSent;
        }
    }
}
