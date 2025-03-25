using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Abstractions.Services
{
    public interface IEventEntityService : IService<EventEntity>
    {
       Task<IEnumerable<EventEntity>> GetFilteredEventsAsync(EventQueryDto query);
    }
}
