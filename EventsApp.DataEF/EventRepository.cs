using EventsApp.Data;
using EventsApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.DataEF
{
    public class EventRepository : IEventRepository
    {
        readonly IBaseRepository<Event> _repository;

        public EventRepository(IBaseRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(Event eventModel)
        {
            await _repository.AddAsync(eventModel);
            return eventModel.Id;
        }

        public Task DeleteAsync(int id)
        {
            return _repository.RemoveAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _repository.Table.Include(x => x.User).ToListAsync();
        }

        public async Task<Event> GetAsync(int id)
        {
            return await _repository.Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Event eventModel)
        {
            return _repository.UpdateAsync(eventModel);
        }
    }
}
