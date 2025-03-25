using Microsoft.AspNetCore.Http;
using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class RegistrationService : Service<Registration>, IRegistrationService
    {
        public RegistrationService(IRepository<Registration> repository, IHttpContextAccessor httpContextAccessor)
        : base(repository, httpContextAccessor)
        {
        }
    }
}
