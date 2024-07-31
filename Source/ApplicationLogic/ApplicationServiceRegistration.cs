using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public static class ApplicationServiceRegistration
    {
        public static void AddApplication(IServiceCollection services)
        {
            services
                // TODO:: .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                // TODO:: CHECK DEPENDENCIES AGAIN ! SHOULD THIS BE ADDED HERE
                // Will the Parsing logic be executed here.
                // .AddSingleton<ISwiftMT799Parser, SwiftMT799Parser>();
        }
    }
}
