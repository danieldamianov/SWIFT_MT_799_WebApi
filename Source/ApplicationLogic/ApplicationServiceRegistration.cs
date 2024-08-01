using ApplicationLogic.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SWIFT_MT799_Logic;
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
                .AddSingleton<ISwiftMT799Parser, SwiftMT799Parser>()
                .AddAutoMapper(typeof(MappingProfile)); ;
        }
    }
}
