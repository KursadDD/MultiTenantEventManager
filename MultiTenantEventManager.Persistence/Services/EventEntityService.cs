using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class EventEntityService : Service<EventEntity>, IEventEntityService
    {

        public EventEntityService(IRepository<EventEntity> repository, IHttpContextAccessor httpContextAccessor)
        : base(repository, httpContextAccessor)
        {
        }

        public async Task<IEnumerable<EventEntity>> GetFilteredEventsAsync(EventQueryDto query)
        {
            var events = Queryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
                events = events.Where(e => e.Title.Contains(query.Title));

            if (!string.IsNullOrWhiteSpace(query.Description))
                events = events.Where(e => e.Description.Contains(query.Description));

            if (query.StartDate.HasValue)
                events = events.Where(e => e.Date >= query.StartDate.Value);

            if (query.EndDate.HasValue)
                events = events.Where(e => e.Date <= query.EndDate.Value);

            if (!string.IsNullOrWhiteSpace(query.Location))
                events = events.Where(e => e.Location.Contains(query.Location));

            if (query.Capacity.HasValue)
                events = events.Where(e => e.Capacity >= query.Capacity.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                bool descending = query.SortOrder?.ToLower() == "desc";

                events = query.SortBy.ToLower() switch
                {
                    "date" => descending ? events.OrderByDescending(e => e.Date) : events.OrderBy(e => e.Date),
                    "title" => descending ? events.OrderByDescending(e => e.Title) : events.OrderBy(e => e.Title),
                    _ => events
                };
            }

            events = events.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

            return await events.ToListAsync();
        }
    }
}
