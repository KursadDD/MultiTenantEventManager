using MultiTenantEventManager.Domain.Entities.Common;
using MultiTenantEventManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Domain.Entities
{
    public class Registration : TenantBaseEntity
    {
        [MaxLength(200), Required]
        public string FullName { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        [ForeignKey("EventEntity")]
        public int EventId { get; set; }
        public EventEntity Event { get; set; }
        public RegistrationStatusType Status { get; set; } = RegistrationStatusType.Waiting;
    }
}
