using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.ResponseMessage;
using MultiTenantEventManager.Domain.Entities;
using MultiTenantEventManager.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class TenantService : ITenantService
    {
        private readonly AppDbContext _context;
        public TenantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenantAsync()
        {
            return await _context.Set<Tenant>().ToListAsync();
        }

        public async Task<ResponseResult> CreateTenantAsync(Tenant tenant)
        {
            var isExist = await _context.Set<Tenant>().AnyAsync(t => t.Name == tenant.Name);

            if (isExist)
            {
                throw new KeyNotFoundException($"Bu isimde başka bir tenant vardır");
            }

            await _context.Set<Tenant>().AddAsync(tenant);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseResult
                {
                    IsSuccess = true,
                    Message = "Kayıt başarılı"
                };
            }

            return new ResponseResult
            {
                IsSuccess = false,
                Message = "Kayıt başarısız"
            };
        }





    }
}
