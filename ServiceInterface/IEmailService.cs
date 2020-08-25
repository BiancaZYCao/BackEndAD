using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace BackEndAD.ServiceInterface
{
    public interface IEmailService
    {
        public Task<String> SendMail(string toAddress, string subject, string body);

    }
}
