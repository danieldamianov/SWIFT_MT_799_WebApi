
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

        public static void Main(string[] args)
        {
            
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {

                var builder = WebApplication.CreateBuilder(args);

                ConfigureServices(builder);

                builder.Host.UseNLog();

                var app = builder.Build();

                EnsureDataStorageExistence(app);

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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<TextPlainOperationFilter>();
            });

            ApplicationServiceRegistration.AddApplication(builder.Services);
            DatabaseServiceRegistration.AddDatabase(builder.Services);
        }
    }
}
