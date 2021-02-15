using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepAPI
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
            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -- .NEt projelerinde Ioc Container alt yap�s� kurmam�z� sa�l�yorlar. Bu yap�lar AOP ile kod yazma imkan� sa�l�yor.
            // AOP (Aspect Oriented Programming) -- Bir metodun �n�nde, sonunda veya hata verdi�inde �al��an kod par�ac�klar�n� AOP mimarisi ile yaz�yoruz.

            services.AddControllers();
            services.AddSingleton<IProductService, ProductManager>(); // IoC -- E�er biri constructor'da IProductService tipinde bir ba��ml�l�k olu�turulursa onun i�in arka planda ProductManager'� new'le demek.
            services.AddSingleton<IProductDal, EfProductDal>(); 
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
