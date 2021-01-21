using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestProject.API.Middlewares;
using TestProject.API.StartupDependencyInjection;
using TestProject.Storage.DAL;

namespace TestProject.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        private IHostEnvironment CurrentEnvironment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.ConfigureDependencyInjection(Configuration);
            

             if (CurrentEnvironment.IsDevelopment())
             {
                 services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestProject"));
             }
             else
             {
                var connString = CurrentEnvironment.EnvironmentName == "docker" ? 
                    "Server=db;Database=master;User=sa;Password=.=xg2]V5;" :
                    Configuration.GetConnectionString("DefaultConnection");
                
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connString, opt => opt.MigrationsAssembly("TestProject.Storage.Migrations"));
                });
             }
            
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            if (!CurrentEnvironment.IsDevelopment())
            {
                
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.Migrate();
                }
            }

            app.UseRouting();
            
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}