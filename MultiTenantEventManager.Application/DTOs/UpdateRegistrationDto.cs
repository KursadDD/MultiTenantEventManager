using MultiTenantEventManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.DTOs
{
    public class UpdateRegistrationDto
    {
        public RegistrationStatusType Status { get; set; }
    }
}
