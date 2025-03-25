using MultiTenantEventManager.Application.ResponseMessage;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Abstractions.Services
{
    public interface IUserService : IService<User>
    {
        Task<ResponseResult> CreateUserAsync(User entity);
    }
}
