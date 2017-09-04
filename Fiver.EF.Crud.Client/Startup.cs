using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Fiver.EF.Crud.Client.Logger;
using Microsoft.Extensions.Logging;

namespace Fiver.EF.Crud.Client
{
    public class Startup
    {
        public Startup(
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new EfLoggerProvider());
        }

        public void ConfigureServices(
            IServiceCollection services)
        {
            var connection = "Data Source=TAHIR;Initial Catalog=efCrud;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // 1. Add using DI and DbContextOptionsBuilder
            //services.AddScoped(factory =>
            //{
            //    var builder = new DbContextOptionsBuilder<Database>();
            //    builder.UseSqlServer(connection);

            //    return new Database(builder.Options);
            //});

            // 2. Add using extension method
            services.AddDbContext<Database>(options =>
                        options.UseSqlServer(connection)
                               .EnableSensitiveDataLogging());

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}
