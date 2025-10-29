using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppJob.Core.Email
{

    public class BackgroundEmailSender : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackgroundEmailSender> _logger;
        private readonly ConcurrentQueue<EmailMessage> _emailQueue = new ConcurrentQueue<EmailMessage>();
        private readonly List<ScheduledEmail> _scheduledEmails = new List<ScheduledEmail>();
        private readonly PeriodicTimer? _schedulingTimer;
        private readonly TimeSpan _schedulingInterval = TimeSpan.FromMinutes(1); // Check for scheduled emails every minute

        public BackgroundEmailSender(IServiceProvider serviceProvider, ILogger<BackgroundEmailSender> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _schedulingTimer = new PeriodicTimer(_schedulingInterval);
        }

        public void EnqueueEmail(EmailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            _emailQueue.Enqueue(message);
            _logger.LogInformation("Email enqueued for sending. Queue size: {QueueSize}", _emailQueue.Count);
        }

        public void ScheduleEmail(EmailMessage message, DateTimeOffset sendAt)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            _scheduledEmails.Add(new ScheduledEmail { Message = message, SendAt = sendAt });
            _logger.LogInformation("Email scheduled for sending at: {SendAt}", sendAt);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Email Sender service started.");

            try
            {
                await Task.WhenAll(
                    ProcessEmailQueueAsync(stoppingToken),
                    ProcessScheduledEmailsAsync(stoppingToken)
                );
            }
            catch (TaskCanceledException)
            {
                // Expected when the service is stopping
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the Background Email Sender service.");
            }
            finally
            {
                _logger.LogInformation("Background Email Sender service stopped.");
                _schedulingTimer?.Dispose();
            }
        }

        private async Task ProcessEmailQueueAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email queue processing started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_emailQueue.TryDequeue(out var message))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    try
                    {
                        _logger.LogInformation("Attempting to send enqueued email to: {Recipients}", string.Join(", ", message.To.Select(r => r.Email)));
                        var sent = await emailService.SendEmailAsync(message);
                        if (sent)
                        {
                            _logger.LogInformation("Enqueued email sent successfully to: {Recipients}", string.Join(", ", message.To.Select(r => r.Email)));
                            // Optionally log success to a database
                        }
                        else
                        {
                            _logger.LogError("Failed to send enqueued email to: {Recipients}", string.Join(", ", message.To.Select(r => r.Email)));
                            // Optionally implement retry logic or logging of failure details
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while sending enqueued email to: {Recipients}", string.Join(", ", message.To.Select(r => r.Email)));
                        // Optionally implement error handling or retry mechanisms
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); // Wait if the queue is empty
                }
            }
            _logger.LogInformation("Email queue processing stopped.");
        }

        private async Task ProcessScheduledEmailsAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scheduled email processing started.");
            if (_schedulingTimer == null) return;

            while (!stoppingToken.IsCancellationRequested && await _schedulingTimer.WaitForNextTickAsync(stoppingToken))
            {
                var now = DateTimeOffset.UtcNow;
                var emailsToSend = _scheduledEmails.Where(s => s.SendAt <= now).ToList();

                if (emailsToSend.Any())
                {
                    using var scope = _serviceProvider.CreateScope();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    foreach (var scheduledEmail in emailsToSend)
                    {
                        try
                        {
                            _logger.LogInformation("Attempting to send scheduled email to: {Recipients} (Scheduled at: {SendAt})", string.Join(", ", scheduledEmail.Message.To.Select(r => r.Email)), scheduledEmail.SendAt);
                            var sent = await emailService.SendEmailAsync(scheduledEmail.Message);
                            if (sent)
                            {
                                _logger.LogInformation("Scheduled email sent successfully to: {Recipients} (Scheduled at: {SendAt})", string.Join(", ", scheduledEmail.Message.To.Select(r => r.Email)), scheduledEmail.SendAt);
                                // Optionally log success
                            }
                            else
                            {
                                _logger.LogError("Failed to send scheduled email to: {Recipients} (Scheduled at: {SendAt})", string.Join(", ", scheduledEmail.Message.To.Select(r => r.Email)), scheduledEmail.SendAt);
                                // Optionally implement retry or logging
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred while sending scheduled email to: {Recipients} (Scheduled at: {SendAt})", string.Join(", ", scheduledEmail.Message.To.Select(r => r.Email)), scheduledEmail.SendAt);
                            // Optionally implement error handling
                        }
                    }

                    // Remove the sent emails from the scheduled list
                    _scheduledEmails.RemoveAll(s => emailsToSend.Contains(s));
                }
            }
            _logger.LogInformation("Scheduled email processing stopped.");
        }
    }

    internal class ScheduledEmail
    {
        public EmailMessage Message { get; set; } = null!;
        public DateTimeOffset SendAt { get; set; }
    }
}

