using EventsApp.Services.Abstractions;
using EventsApp.Services.Implementations;
using EventsApp.DataEF.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsApp.Services.Extensions
{

    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IEventService, EventService>();

            #region Repositories

            services.AddRepositories();

            #endregion
        }
    }
}
