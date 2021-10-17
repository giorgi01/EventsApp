using EventsApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsApp.Data
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetAsync(int id);
        Task<int> CreateAsync(Event eventModel);
        Task<bool> Exists(int id);
        Task UpdateAsync(Event eventModel);
        Task DeleteAsync(int id);
    }
}
