using MultiTenantEventManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        [MaxLength(50), Required]
        public string Name { get; set; }
    }
}
