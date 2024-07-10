using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvoiceSystem.Services;

namespace InvoiceSystem
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services needed for the application
            services.AddScoped<InvoiceService>(); // Register InvoiceService for dependency injection

            services.AddControllers(); // Add MVC controllers
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed exception information during development
            }

            app.UseRouting(); // Enable routing

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Map controllers to endpoints
            });
        }
    }
}
