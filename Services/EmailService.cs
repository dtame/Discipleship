using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WandaWebAdmin.Services.Contracts;

namespace WandaWebAdmin.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string subject, string bodyMail, string toEmailAddress)
        {  try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient("mail.lechemindudisciple.com");
                    message.From = new MailAddress(EmailAddresses.PostMaster.Address);
                    message.To.Add(new MailAddress(toEmailAddress));
                    message.Subject = subject;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = bodyMail;
                    smtp.Port = 25;
                    //smtp.Host = "mail.lechemindudisciple.com";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(EmailAddresses.PostMaster.Address, EmailAddresses.PostMaster.Password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
        }
    }

    public static class EmailAddresses 
    {
        public static EmailAddress PostMaster 
        { 
            get {
                return new EmailAddress
                {
                    Address = "postmaster@lechemindudisciple.com",
                    Password = "Domnik001"
                };
            }
        }

        public static string Prayer
        {
            get { return "priespourmoi@lechemindudisciple.com"; }
        }

        public static string Give
        {
            get { return "semer@lechemindudisciple.com"; }            
        }
    }

    public class EmailAddress
    {
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
