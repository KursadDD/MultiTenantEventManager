using MultiTenantEventManager.Application.Enums;
using MultiTenantEventManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Domain.Entities
{
    public class User : TenantBaseEntity
    {
        [MaxLength(50), Required]
        public string Name { get; set; }

        [MaxLength(100), Required]
        public string Email { get; set; }

        [MinLength(6), MaxLength(50), Required]
        public string Password { get; set; }

        public UserRoleType Role { get; set; }
    }
}
