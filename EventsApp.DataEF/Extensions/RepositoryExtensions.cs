using EventsApp.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsApp.DataEF.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IAppConfigRepository, AppConfigRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
