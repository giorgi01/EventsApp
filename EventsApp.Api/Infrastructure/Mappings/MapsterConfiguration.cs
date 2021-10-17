using EventsApp.Api.Models.Requests;
using EventsApp.Api.Models.DTOs;
using EventsApp.Domain;
using EventsApp.Services.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsApp.Domain.POCO;

namespace EventsApp.Api.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<Event, EventServiceModel>
                .NewConfig()
                .Map(dest => dest.Author, src => src.User.UserName);

            TypeAdapterConfig<EventServiceModel, Event>
                .NewConfig();

            TypeAdapterConfig<EventServiceModel, EventRequest>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<EventServiceModel, EventDTO>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<EventServiceModel, EventPutRequest>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<AppConfigDTO, AppConfig>
                .NewConfig()
                .TwoWays();
        }
    }
}
