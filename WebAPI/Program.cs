using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)//serverla ilgili configuretion , bir kere yapaars�n bir daha yapmazs�n
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureContainer<ContainerBuilder>(builder => 
                {
                    builder.RegisterModule(new AutofacBusinessModule()); // .NET.Core yerine ba�ka bir IoC container kullanmak istersem bu yap�l�r 
                
                }
                
                )
            //autofaci kullan�cam da autofac i�in yazd. dosyay� nerede vereyim
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
