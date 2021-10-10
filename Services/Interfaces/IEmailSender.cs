using NewBrainfieldNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest message);
    }
}
