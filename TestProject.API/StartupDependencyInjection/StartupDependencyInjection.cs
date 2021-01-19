using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Common.Attribute;

namespace TestProject.API.StartupDependencyInjection
{
    public static class StartupDependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.Scan(scan => scan.FromExecutingAssembly()
                .FromApplicationDependencies(a => a.FullName.Contains("TestProject.Storage.Repositories")
                                                  || a.FullName.Contains("TestProject.Infrastructure")
                                                  || a.FullName.Contains("TestProject.Core"))
                .AddClasses(filter => filter.WithAttribute<ExposeForDIAttribute>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}