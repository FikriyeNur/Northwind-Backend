using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    // Autofac yapısı AOP kullanmamızı sağlar. O yüzden .Net'in IoC yapısı yerine Autofacy apısını oluşturduk. 
    public class AutofacBusinessModule : Module
    {
        // Uygulama hayata geçerken kısaca uygulamayı çalıştırdığımız zaman çalışan metot.
        protected override void Load(ContainerBuilder builder)
        {
            // eğer birisi constructor'da IProductService isterse ProductManager'ı register et yani ProductManager instance'ını ver demek. WepAPI startup içinde oluşturduğumuz yapıyla aynı. Tek fark orda .Net IoC kullanıyorduk burda Autofac sistemini kullanıyoruz.
            // SingleInstance ile tek bir instance alıp her yerde onu kullanırız. Referans tip herkese aynı referansı verir.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();  

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
