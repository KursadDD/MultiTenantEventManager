using MultiTenantEventManager.Application.ResponseMessage;
using MultiTenantEventManager.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Application.Abstractions.Services
{
    public interface IService<T> where T : TenantBaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task<ResponseResult> CreateAsync(T entity);
        Task<ResponseResult> UpdateAsync(T entity);
        Task<ResponseResult> DeleteAsync(int id);
        IQueryable<T> Queryable();
    }
}
