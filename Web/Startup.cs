using BlazorDownloadFile;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using BundlerMinifier.TagHelpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Web.Components.Http;
using Web.Components.Session;
using Westwind.AspNetCore.LiveReload;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("external-api", httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("external-api"));
            });
            services.AddScoped<IHttpRequestBuilderFactory, HttpRequestBuilderFactory>();

            services.AddLiveReload();
            services.AddBundles(options =>
            {
                options.AppendVersion = true;
            });
            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
                options.DelayTextOnKeyPress = true;
                options.DelayTextOnKeyPressInterval = 300;
            });
            services.AddBootstrapProviders();
            services.AddBlazorDownloadFile();

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options =>
            {
                options.DetailedErrors = true;
            });
            services.AddAuthorization();
            services.AddAuthentication();
            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider, SessionProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseLiveReload();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}