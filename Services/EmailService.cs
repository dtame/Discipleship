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
        public bool SendEmail(string subject, string bodyMail, string from, string toEmailAddress)
        {  try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(EmailAddresses.PostMaster.Address);
                    mail.To.Add(EmailAddresses.PostMaster.Address);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    mail.Body = bodyMail;

                    SmtpClient smtp = new SmtpClient("mail.lechemindudisciple.com");                                   
                    smtp.Credentials = new NetworkCredential(EmailAddresses.PostMaster.Address, EmailAddresses.PostMaster.Password);
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 8889;
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                     return true;
                }
                catch (Exception ex)
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
                    Password = "Domnik.001"
                };
            }
        }

        public static string Infos
        {
            get { return "infos@lechemindudisciple.com"; }
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
