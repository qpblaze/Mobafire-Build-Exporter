using LoLSets.Core.Interfaces;
using LoLSets.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LoLSets.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IChampionService, ChampionService>();
            services.AddScoped<IMobafireService, MobafireService>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // For better security
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // Forces the browser to reconnect to HTTPS
            app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());

            // Turns on cross site scripting prevention measures
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            // Prevents attacks with different content type
            app.UseXContentTypeOptions();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            
        }
    }
}