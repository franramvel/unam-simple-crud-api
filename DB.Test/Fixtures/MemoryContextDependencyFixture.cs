
using DB.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace DB.Test.Fixtures
{
    public class MemoryContextDependencyFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public MainDbContext Context { get; private set; }
        public IServiceScopeFactory ServiceScopeFactory { get; private set; }


        public MemoryContextDependencyFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            //IMPORTANTE:
            //Unicamente levantar esta varible de entorno en local, si se desea hacer pruebas sobre la base de datos productiva, se debe utilizar un SecretsManager 
            Environment.SetEnvironmentVariable("CONNECTION_STRING", "Data Source=localhost;Initial Catalog=Empleados;Integrated Security=True;TrustServerCertificate=True;");
            var cnx = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
            serviceCollection
                .AddDbContext<MainDbContext>(async options =>
                options.UseSqlServer(cnx, providerOptions =>
                {
                    providerOptions.CommandTimeout(180);
                }),
                    ServiceLifetime.Transient);
            serviceCollection.AddScoped<IServiceScopeFactory>(provider =>
            {
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
                return scopeFactory;
            });
            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();


            Context = ServiceProvider.GetService<MainDbContext>()!;

        }






    }
}
