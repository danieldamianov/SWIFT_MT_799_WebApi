using ApplicationLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SWIFT_MT799_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DatabaseServiceRegistration
    {
        public static void AddDatabase(IServiceCollection services)
        {
            services
                .AddSingleton<ISWIFT_MT799_WebApiDataProvider, SWIFT_MT799_WebApi_SQLiteDataProvider>();
        }
    }
}
