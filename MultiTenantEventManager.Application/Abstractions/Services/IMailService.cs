using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task<bool> SendMail(string? email);
    }
}
