using MultiTenantEventManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.ResponseMessage
{
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
