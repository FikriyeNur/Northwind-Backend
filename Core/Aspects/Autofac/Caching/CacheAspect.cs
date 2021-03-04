using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        // biz süre vermezsek 60 dk boyunca cache de kalır. sonrasında sistem otomatik olarak cache'den (bellekten) atar.
        public CacheAspect(int duration = 60) 
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        // {invocation.Method.ReflectedType.FullName} --> Business.Abstract.IProductService.{invocation.Method.Name} --> GetAll şeklinde ikisini birleştirip bir key oluşturduk.
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList(); // method'un parametresi varsa listele
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";    // eğer method'un parametre değeri varsa o parametreye göre yukarıdaki GetAll() içine ekle. parametre yoksa boş geçer.
            if (_cacheManager.IsAdd(key)) // bellekteki cache anahtarına bakıyoruz 
            {
                invocation.ReturnValue = _cacheManager.Get(key); // cache'de veri varsa direk bellekten almak yerine bunu döndürürüz.
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // cache'de yoksa veritabanından datayı alırız. ve gelen veriyi cache'leriz ki bir sonraki sefer onu kullanalım.
        }
    }
}
