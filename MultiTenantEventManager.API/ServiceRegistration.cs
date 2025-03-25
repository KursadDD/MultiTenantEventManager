using MultiTenantEventManager.Application.Abstractions.Repositories;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.Mapping;
using MultiTenantEventManager.Infrastructure.Services;
using MultiTenantEventManager.Persistence.Repositories;
using MultiTenantEventManager.Persistence.Services;

namespace MultiTenantEventManager.API
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IEventEntityService, EventEntityService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMailService, MailService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
