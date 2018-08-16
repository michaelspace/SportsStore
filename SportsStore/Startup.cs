using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();

            services.AddScoped<Cart>(s => SessionCart.GetCart(s)); // ten sam obiekt bedzie uzyty do spelnienia powiazanych requests dla egzemplarzy Cart
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // w celu uzyskania dostepu do sesji z SessionCart

            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles(); // włącza obsługę treści statycznej znajdującej się w katalogu wwwroot
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Strona{productPage:int}",
                    defaults: new { controller = "Product", action = "List"}
                );

                routes.MapRoute(
                    name: null,
                    template: "Strona{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}"
                );
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
