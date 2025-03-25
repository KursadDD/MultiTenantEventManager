using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.ResponseMessage;
using MultiTenantEventManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class Service<T> : IService<T> where T : TenantBaseEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Service(IRepository<T> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (IsSuperAdmin())
                return await _repository.Queryable().FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);

            return await _repository.Queryable().FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted && f.TenantId == GetTenantId());

        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            if (IsSuperAdmin())
                return await _repository.Queryable().Where(f => !f.IsDeleted).ToListAsync();

            return await _repository.Queryable().Where(f => !f.IsDeleted && f.TenantId == GetTenantId()).ToListAsync();
        }

        public async Task<ResponseResult> CreateAsync(T entity)
        {
            entity.TenantId = GetTenantId();

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

        public async Task<ResponseResult> UpdateAsync(T entity)
        {
            ResponseResult responseResult = new();
            if (entity.Id == 0)
            {
                return new ResponseResult
                {
                    IsSuccess = false,
                    Message = "Kayıt bulunamadı"
                };
            }

            if (!IsSuperAdmin() && entity.TenantId != GetTenantId())
            {
                return new ResponseResult
                {
                    IsSuccess = false,
                    Message = "Kaydı güncelleme yetkiniz yoktur."
                };

            }
            entity.UpdatedAt = DateTime.Now;
            var result = await _repository.UpdateAsync(entity);

            if (result)
                return new ResponseResult
                {
                    IsSuccess = true,
                    Message = "Kayıt Güncellendi"
                };

            return new ResponseResult
            {
                IsSuccess = false,
                Message = "Güncelleme Başarısız"
            };
        }

        public async Task<ResponseResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return new ResponseResult
                {
                    IsSuccess = false,
                    Message = "Kayıt bulunamadı"
                };
            }

            var entity = await _repository.Queryable().FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);
            if (entity == null)
            {
                return new ResponseResult
                {
                    IsSuccess = false,
                    Message = "Kayıt bulunamadı"
                };
            }

            if (!IsSuperAdmin() && entity.TenantId != GetTenantId())
            {
                return new ResponseResult
                {
                    IsSuccess = false,
                    Message = "Kaydı silme yetkiniz yoktur."
                };
            }
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            var result = await _repository.DeleteAsync(entity);
            if (result)
                return new ResponseResult
                {
                    IsSuccess = true,
                    Message = "Kayıt Silindi"
                };

            return new ResponseResult
            {
                IsSuccess = false,
                Message = "Silme Başarısız"
            };
        }

        public IQueryable<T> Queryable()
        {
            return _repository.Queryable();
        }

        private int GetTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == "tenantId")?.Value;
            return tenantId == null ? 0 : int.Parse(tenantId);
        }

        private bool IsSuperAdmin()
        {
            var userRole = _httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return userRole == "SuperAdmin";
        }

    }

}
