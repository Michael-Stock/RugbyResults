using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RugbyResults.Configuration;
using RugbyResults.DAL.Matches;
using RugbyResults.Matches;
using RugbyResults.Warmup;

namespace RugbyResults
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            services.Configure<ExternalApiOptions>(Configuration.GetSection(ExternalApiOptions.ExternalApi));

            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchDal, MatchDal>();
            services.AddScoped<IWarmupService, WarmupService>();

            // Trigger the bootstrap
            services.AddHostedService<Bootstrap>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
