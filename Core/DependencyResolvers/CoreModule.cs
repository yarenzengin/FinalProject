using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule // uyg.sev.de servis bağımlılıklarımızı çözeceğimiz yer
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();//== IMemoryCache _memoryCache; injection yapıyor --- arka planda hazır bir ICachManager instance oluşturuyor
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//arka planda ımemorycachin instanceını oluşturuyor
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
