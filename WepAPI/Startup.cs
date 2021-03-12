using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
            // Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -- .Net projelerinde IoC Container alt yap�s� kurmam�z� sa�l�yorlar. Bu yap�lar AOP ile kod yazma imkan� sa�l�yor.
            // AOP (Aspect Oriented Programming) -- Bir metodun �n�nde, sonunda veya hata verdi�inde �al��an kod par�ac�klar�n� AOP mimarisi ile yaz�yoruz.
            // Postsharp

            services.AddControllers();

            // Business Katman�nda olu�turdu�umuz Autofac yap�s� bu IoC'nin kar��l���d�r.
            //services.AddSingleton<IProductService, ProductManager>(); // IoC -- E�er biri constructor'da IProductService tipinde bir ba��ml�l�k olu�turulursa onun i�in arka planda ProductManager'� new'le demek.
            //services.AddSingleton<IProductDal, EfProductDal>();

            services.AddCors();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            services.AddDependencyResolvers(new CoreModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // bu adresten gelen isteklere izin verdik. Frontend'i ba�ka port da a�arsak �al��maz sadece bu adrese izin verdik.
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // middleware --> asp.net ya�am d�ng�s�nde hangi yap�lar�n s�ras�yla devreye girece�ini s�yleriz.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
