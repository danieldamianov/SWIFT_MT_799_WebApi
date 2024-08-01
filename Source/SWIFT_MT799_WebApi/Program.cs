
using ApplicationLogic.Interfaces;
using ApplicationLogic;
using SWIFT_MT799_Logic;
using System.Reflection;
using Database;

namespace SWIFT_MT799_WebApi
{
    public class Program
    {
        // TODO:: ADD logging
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            ConfigureServices(builder);

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
            builder.Services.AddSwaggerGen();

            // TODO:: CHECK IF THIS WORKS FINE
            ApplicationServiceRegistration.AddApplication(builder.Services);
            DatabaseServiceRegistration.AddDatabase(builder.Services);
            // builder.Services.AddSingleton<ISwiftMT799Parser, SwiftMT799Parser>();
            // TODO:: AddAplication
        }
    }
}
