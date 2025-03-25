using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Enums
{
    public enum UserRoleType
    {
        SuperAdmin = 1,
        TenantAdmin = 2,
        EventManager = 3,
        Attendee = 4,
    }
}
