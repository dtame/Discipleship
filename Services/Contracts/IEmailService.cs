using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WandaWebAdmin.Services.Contracts
{
    public interface IEmailService
    {
        bool SendEmail(string subject, string bodyMail, string toEmailAddress);
    }
}
