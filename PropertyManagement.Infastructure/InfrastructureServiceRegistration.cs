using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertyManagement.Application.Contracts.Infastructure;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Models;
using PropertyManagement.Infastructure.Mail;
using PropertyManagement.Infastructure.Persistence;
using PropertyManagement.Infastructure.Repositories;

namespace PropertyManagement.Infastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PropertyContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PropertyConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
