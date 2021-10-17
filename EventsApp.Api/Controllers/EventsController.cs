using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EventsApp.Services.Models;
using System.Threading.Tasks;
using EventsApp.Services.Abstractions;
using Mapster;
using EventsApp.Api.Models.DTOs;
using EventsApp.Api.Models.Requests;
using EventsApp.Data;
using PagedList;
using System;

namespace EventsApp.Api.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _service;
        private readonly IUserRepository _userRepository;
        public EventsController(IEventService service,
                                IUserRepository userRepository)
        {
            _service = service;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Returns all events
        /// </summary>
        /// <remarks>
        /// Event, which !IsArchived
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var eventServiceModels = await _service.GetAllAsync();
            return Ok(eventServiceModels.Adapt<List<EventDTO>>());
        }

        /// <summary>
        /// Returns published events
        /// </summary>
        /// <remarks>
        /// Event, which IsActive && !IsArchived
        /// </remarks>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetActiveEvents()
        {
            var eventServiceModels = await _service.GetActiveEventsAsync();
            var result = eventServiceModels.Adapt<List<EventDTO>>();
            return Ok(result);
        }

        /// <summary>
        /// Returns user-related events
        /// </summary>
        /// <param name="userId">
        /// Parameter type: string,
        /// Example: userId with events -> 1d3d5b95-44af-4d3e-9912-4c531c6d7143
        /// </param>
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetEvents(string userId)
        {
            var eventServiceModels = await _service.GetUserEventsAsync(userId);
            return Ok(eventServiceModels.Adapt<List<EventDTO>>());
        }

        /// <summary>
        /// Returns unpublished events
        /// </summary>
        /// <remarks>
        /// Event, which !IsActive && !IsArchived
        /// </remarks>        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUnpublishedEvents()
        {
            var eventServiceModels = await _service.GetUnpublishedEventsAsync();
            return Ok(eventServiceModels.Adapt<List<EventDTO>>());
        }

        /// <summary>
        /// Returns archived events
        /// </summary>
        /// <remarks>
        /// Event, which IsArchived
        /// </remarks>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetArchivedEvents()
        {
            var eventServiceModels = await _service.GetArchivedEventsAsync();
            return Ok(eventServiceModels.Adapt<List<EventDTO>>());
        }


        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="eventRequest">Event model</param>
        [HttpPost]
        public async Task<IActionResult> Post(EventRequest eventRequest)
        {
            var eventSM = eventRequest.Adapt<EventServiceModel>();
            var (status, id) = await _service.CreateAsync(eventSM);
            if (status == Status.Success) return Ok(id);
            return NotFound();
        }

        /// <summary>
        /// Returns event
        /// </summary>
        /// <param name="id">Event id</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            if (result.status == Status.Success) return Ok(result.Item2.Adapt<EventDTO>());
            return NotFound();
        }

        /// <summary>
        /// Update event
        /// </summary>
        /// <param name="eventRequest">Event model</param>
        [HttpPut]
        public async Task<IActionResult> Put(EventPutRequest eventRequest)
        {
            var eventSM = eventRequest.Adapt<EventServiceModel>();
            var status = await _service.UpdateAsync(eventSM);
            if (status == Status.Success) return Ok();
            return Problem();
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="id">Event id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _service.DeleteAsync(id);
            if (status == Status.Success) return Ok();
            return Problem();
        }
    }
}
