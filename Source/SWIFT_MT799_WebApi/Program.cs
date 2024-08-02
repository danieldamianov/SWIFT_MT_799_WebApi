
using ApplicationLogic.Interfaces;
using ApplicationLogic;
using SWIFT_MT799_Logic;
using System.Reflection;
using Database;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

namespace SWIFT_MT799_WebApi
{
    public class Program
    {
        // TODO:: ADD logging
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                ConfigureServices(builder);

                // NLog: Setup NLog for Dependency injection
                //builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                var app = builder.Build();

                EnsureDataStorageExistence(app);

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static void EnsureDataStorageExistence(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var myService = scope.ServiceProvider.GetRequiredService<ISWIFT_MT799_WebApiDataProvider>();
                myService.EnsureDataStorageExists();
            }
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<TextPlainOperationFilter>(); // Register the custom filter
            });

            // TODO:: CHECK IF THIS WORKS FINE
            ApplicationServiceRegistration.AddApplication(builder.Services);
            DatabaseServiceRegistration.AddDatabase(builder.Services);
            // builder.Services.AddSingleton<ISwiftMT799Parser, SwiftMT799Parser>();
            // TODO:: AddAplication
        }
    }
}
