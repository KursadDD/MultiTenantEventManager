using Microsoft.AspNetCore.Http;
using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.ResponseMessage;
using MultiTenantEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository, IHttpContextAccessor httpContextAccessor)
        : base(repository, httpContextAccessor)
        {
            _repository = repository;
        }

        public async Task<ResponseResult> CreateUserAsync(User entity)
        {
            var result = await _repository.CreateAsync(entity);
            if (result)
                return new ResponseResult
                {
                    IsSuccess = true,
                    Message = "Kayıt oluşturuldu"
                };

            return new ResponseResult
            {
                IsSuccess = false,
                Message = "Kayıt Başarısız"
            };
        }
    }
}
