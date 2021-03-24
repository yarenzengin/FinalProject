using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{//web api de veya autofac te oluşturuduğumuz injectionları oluşturabilmemize yarıyor, ist. herhangi bir interfacein servis karşılığını bu tool ile alabiliriz
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)//.net in servislerini al ve build et 
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
