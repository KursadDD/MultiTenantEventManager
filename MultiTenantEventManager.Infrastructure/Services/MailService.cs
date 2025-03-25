using MultiTenantEventManager.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Infrastructure.Services
{
    public class MailService : IMailService
    {
        public Task<bool> SendMail(string? email)
        {
            //mail başarı ile yollandı
            return Task.FromResult(true);
        }
    }
}
