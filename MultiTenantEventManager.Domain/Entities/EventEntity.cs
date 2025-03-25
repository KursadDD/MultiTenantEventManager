using MultiTenantEventManager.Domain.Entities.Common;
using MultiTenantEventManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Domain.Entities
{
    public class EventEntity : TenantBaseEntity
    {
        [MaxLength(100), Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public EventStatusType Status { get; set; } = EventStatusType.Active;
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
