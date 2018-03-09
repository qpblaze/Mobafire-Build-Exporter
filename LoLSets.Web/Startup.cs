using LoLSets.Core.Interfaces;
using LoLSets.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LoLSets.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IChampionService, ChampionService>();
            services.AddScoped<IMobafireService, MobafireService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            
        }
    }
}