using desafio_rdi.cross_cutting;
using desafio_rdi.domain.Repositories;
using desafio_rdi.domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace desafio_rdi.webapi.Configuration
{
    public static class RegisterDIConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddDbContext<DesafioDbContext>(opt => opt.UseInMemoryDatabase("default"));
            services.AddScoped<IDesafioRepository, DesafioRepository>();
            services.AddScoped<ICustomerCardService, CustomerCardService>();
            services.AddScoped<INotification, Notification>();

            return services;
        }
    }
}
