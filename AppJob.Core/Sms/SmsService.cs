using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Data;
using Microsoft.Extensions.Configuration;
using Kavenegar.Core;
using Kavenegar.Core.Models;
using AppJob.Core.Domain.Entities;
using SendResult = Kavenegar.Core.Models.SendResult;

namespace AppJob.Core.Sms
{
    
    public class SmsService : ISmsService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly MyServiceDbContext _dbContext;
        public SmsService(MyServiceDbContext dbContext, IConfiguration config, HttpClient httpClient)
        {
            _apiKey = config["Kavenegar:ApiKey"];
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<string> SendSmsAsync(string from,string to, string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_apiKey);

            SendResult result = await api.Send(from, to, message);

            foreach (var toPerson in to)
            {
                var logs = new SMSLog()
                {
                    from = new List<string>() { from },
                    to = new List<string>() { to },
                    messages = new List<string>() { message },
                };
                _dbContext.SMSLogs.Add(logs);
                _dbContext.SaveChanges();
            }

            var reconrd = new Domain.Entities.SendResult();
            
            reconrd.Cost = result.Cost;
            reconrd.Date = result.Date;
            reconrd.Message = result.Message;
            reconrd.Messageid = result.Messageid;
            reconrd.Receptor = result.Receptor;
            reconrd.Sender = result.Sender;
            reconrd.Status = result.Status;
            reconrd.StatusText = result.StatusText;

            _dbContext.SendResults.Add(reconrd);
            _dbContext.SaveChanges();
            return result.Status.ToString();
        }

        public async Task<string> SendSmsAsync(List<String> from,List<string> to, List<string> messages)
        {
            if (to == null || messages == null || to.Count != messages.Count)
                throw new ArgumentException("تعداد شماره‌ها و پیام‌ها باید برابر باشد.");

            List<SendResult> result = new List<SendResult>();

            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_apiKey);
            result = await api.SendArray(from, to ,messages);

            foreach (var toPerson in to)
            {
                var logs = new SMSLog()
                {
                    from = from ,
                    to = to,
                    messages = messages ,
                };

                ;
                _dbContext.SMSLogs.Add(logs);
                _dbContext.SaveChanges();
            }
            foreach (var sendResult in result)
            {
                var reconrd = new Domain.Entities.SendResult();
                reconrd.Cost = sendResult.Cost;
                reconrd.Date = sendResult.Date;
                reconrd.Message = sendResult.Message;
                reconrd.Messageid = sendResult.Messageid;
                reconrd.Receptor = sendResult.Receptor;
                reconrd.Sender = sendResult.Sender;
                reconrd.Status = sendResult.Status;
                reconrd.StatusText = sendResult.StatusText;

                _dbContext.SendResults.Add(reconrd);
                _dbContext.SaveChanges();
            }
            return string.Join("\n", result);
        }


        public async Task<List<SendResult>> SendBulkSmsAsync(string from, List<string> to, string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_apiKey);
            List<SendResult> result = await api.Send(from, to, message);

            foreach (var toPerson in to)
            {
                var logs = new SMSLog()
                {
                    from = new List<string>(){ from },
                    to = to,
                    messages = new List<string>() { message}, 
                };

            ;
            _dbContext.SMSLogs.Add(logs);
            _dbContext.SaveChanges();
            }
            foreach (var sendResult in result)
            {
                var reconrd = new Domain.Entities.SendResult();
                reconrd.Cost = sendResult.Cost;
                reconrd.Date = sendResult.Date;
                reconrd.Message = sendResult.Message;
                reconrd.Messageid = sendResult.Messageid;
                reconrd.Receptor = sendResult.Receptor;
                reconrd.Sender = sendResult.Sender;
                reconrd.Status = sendResult.Status;
                reconrd.StatusText = sendResult.StatusText;

                _dbContext.SendResults.Add(reconrd);
                _dbContext.SaveChanges();
            }
            return result;
        }
    }
}
