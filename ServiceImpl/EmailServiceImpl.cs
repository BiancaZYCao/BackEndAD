using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using BackEndAD.ServiceInterface;

namespace BackEndAD.ServiceImpl
{
    public class EmailServiceImpl:IEmailService
    {
        public async Task<String> SendMail(string toAddress, string subject, string body)
        {
            String senderAddress = "gdipsa50t8@gmail.com";
            String senderPwd = "@newpass8";
            try
            {
                string FromAddress = senderAddress;
                string FromAdressTitle = "Team8 AD";
                //To Address    
                string ToAddress = toAddress;
                string ToAdressTitle = "Testing Team8 mail";
                string Subject = subject;
                string BodyContent = body;

                //Smtp Server    
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number    
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAdressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAdressTitle,
                                         ToAddress
                                         ));
                mimeMessage.Subject = Subject; //Subject  
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        senderAddress,
                        senderPwd
                        );
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Email Sending Failed!!!";
            }
            return "Email Sending Success";
        }
    }
}
