using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
   public static class ServiceCollectionExtensions
    { //apinin servis bağımlılıklarını eklediğimiz,araya girmesini isted.servisleri eklediğimiz koleksiyon  
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules) {

            foreach (var module in modules)//birden fazla module koyabiliriz
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);//servicetool.create(services) >> bu bir tane eleman için geçerliydi
      //core katmanı dahil bütün injectionları bir araya toplayabileceğimiz bir yapıya dönüştü

        }

           
    }
}
