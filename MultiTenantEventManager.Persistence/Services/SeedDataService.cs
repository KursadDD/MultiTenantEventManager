using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiTenantEventManager.Application.Enums;
using MultiTenantEventManager.Domain.Entities;
using MultiTenantEventManager.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantEventManager.Persistence.Services
{
    public class SeedDataService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedDataService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    int tenantId = 0;
                    if (!dbContext.Tenants.Any())
                    {
                        var defaultTenant = new Tenant
                        {
                            Name = "DefaultTenant",
                        };

                        dbContext.Tenants.Add(defaultTenant);
                        await dbContext.SaveChangesAsync(cancellationToken);
                        tenantId = defaultTenant.Id;
                    }

                    if (!dbContext.Users.Any(u=>u.Role == UserRoleType.SuperAdmin) && tenantId!=0 )
                    {
                        var superAdmin = new User
                        {
                            Name = "Super Admin",
                            Email = "superadmin@gmail.com",
                            Password = "123456",
                            Role = UserRoleType.SuperAdmin,
                            TenantId = tenantId,
                        };

                        dbContext.Users.Add(superAdmin);
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
