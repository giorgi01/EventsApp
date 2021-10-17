using EventsApp.Data;
using EventsApp.Domain;
using EventsApp.Services.Abstractions;
using EventsApp.Services.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApp.Services.Implementations
{
    public class EventService : IEventService
    {
        #region Private Members

        private readonly IEventRepository _repo;
        private readonly IUserRepository _userRepository;
        private readonly IAppConfigRepository _configRepository;

        #endregion

        #region Ctor
        public EventService(IEventRepository repo, IAppConfigRepository configRepository, IUserRepository userRepository)
        {
            _repo = repo;
            _configRepository = configRepository;
            _userRepository = userRepository;
        }
        #endregion

        public async Task<List<EventServiceModel>> GetAllAsync()
        {
            var result = (await _repo.GetAllAsync()).Where(x => !x.IsArchived);

            return result.Adapt<List<EventServiceModel>>();
        }

        public async Task<List<EventServiceModel>> GetArchivedEventsAsync()
        {
            var result = (await _repo.GetAllAsync()).Where(x => x.IsArchived);

            return result.Adapt<List<EventServiceModel>>();
        }

        public async Task<(Status, EventServiceModel)> GetAsync(int id)
        {
            var result = await _repo.GetAsync(id);

            if (result == null)
                return (Status.NotFound, null);

            return (Status.Success, result.Adapt<EventServiceModel>());
        }
        public async Task<(Status, int)> CreateAsync(EventServiceModel eventSM)
        {
            eventSM.CreatedAt = DateTime.Now;
            var eventToInsert = eventSM.Adapt<Event>();

            int insertedId = await _repo.CreateAsync(eventToInsert);

            return (Status.Success, insertedId);
        }
        public async Task<Status> DeleteAsync(int id)
        {
            var eventModel = await _repo.GetAsync(id);

            if (eventModel == null)
                return Status.NotFound;

            await _repo.DeleteAsync(id);

            return Status.Success;
        }
        public async Task<Status> UpdateAsync(EventServiceModel eventSM)
        {

            if (!(await _repo.Exists(eventSM.Id)))
                return Status.NotFound;

            var eventToUpdate = eventSM.Adapt<Event>();
            await _repo.UpdateAsync(eventToUpdate);

            return Status.Success;
        }

        public async Task<List<EventServiceModel>> GetActiveEventsAsync()
        {
            var result = await _repo.GetAllAsync();

            result = result.Where(x => x.IsActive && (!x.IsArchived)).ToList();
            return result.Adapt<List<EventServiceModel>>();
        }

        public async Task<List<EventServiceModel>> GetUnpublishedEventsAsync()
        {
            var result = await _repo.GetAllAsync();

            result = result.Where(x => !x.IsActive && (!x.IsArchived)).ToList();
            return result.Adapt<List<EventServiceModel>>();
        }

        public async Task<List<EventServiceModel>> GetUserEventsAsync(string id)
        {
            var user = await _userRepository.GetAsync(id);
            return user.Events.Where(x => !x.IsArchived).Adapt<List<EventServiceModel>>();
        }
    }
}
