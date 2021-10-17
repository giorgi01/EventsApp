using EventsApp.Domain.POCO;
using EventsApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.Services.Abstractions
{
    public interface IEventService
    {
        Task<List<EventServiceModel>> GetAllAsync();
        Task<(Status status, EventServiceModel)> GetAsync(int id);
        Task<(Status status, int id)> CreateAsync(EventServiceModel eventSM);
        Task<Status> UpdateAsync(EventServiceModel eventSM);
        Task<Status> DeleteAsync(int id);
        Task<List<EventServiceModel>> GetActiveEventsAsync();
        Task<List<EventServiceModel>> GetUserEventsAsync(string id);
        Task<List<EventServiceModel>> GetArchivedEventsAsync();
        Task<List<EventServiceModel>> GetUnpublishedEventsAsync();
    }
}
