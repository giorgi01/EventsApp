using EventsApp.Domain.POCO;
using EventsApp.Mvc.Helpers;
using EventsApp.Mvc.Models;
using EventsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace EventsApp.Mvc.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "MustBeLoggedIn")]
    public class EventsController : Controller
    {
        private readonly HttpClient _client;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(EventsAPI api, UserManager<ApplicationUser> userManager)
        {
            _client = api.Client;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var response = await _client.GetAsync("Events/GetActiveEvents");
            var indexVM = new List<EventViewModel>();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                indexVM = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
            }
            return View(indexVM.ToPagedList(pageNumber, 3));
        }

        [Route("[action]")]
        public async Task<IActionResult> UserEvents(int? page)
        {
            var user = await _userManager.GetUserAsync(User);
            var response = await _client.GetAsync($"Events/GetEvents/{user.Id}");
            var pageNumber = page ?? 1;
            var indexVM = new List<EventViewModel>();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                indexVM = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
            }
            return View(indexVM.ToPagedList(pageNumber, 3));
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(EventCreateViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                eventVM.UserId = (await _userManager.GetUserAsync(User)).Id;
                var response = await _client.PostAsJsonAsync("Events", eventVM);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(UserEvents));
                else
                    await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
            }
            ModelState.AddModelError(string.Empty, "Invalid event creation attempt");
            return View();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var eventVM = await GetEventByIdAsync(id);
            return View(eventVM);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventVM = await GetEventByIdAsync(id);
            return View(eventVM);
        }


        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Edit(EventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var configResponse = await _client.GetAsync("AppConfigs/MinutesAfterThatEventChangeIsProhibited");
                if (configResponse.IsSuccessStatusCode)
                {
                    var configResponseContent = configResponse.Content.ReadAsStringAsync().Result;
                    var config = JsonConvert.DeserializeObject<AppConfig>(configResponseContent);

                    if (eventVM.CreatedAt + TimeSpan.FromMinutes(config.Value) > DateTime.Now)
                    {
                        var response = await _client.PutAsJsonAsync("Events", eventVM);
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction(nameof(UserEvents));
                        else
                            await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
                    }
                    ModelState.AddModelError(string.Empty, "Action prohibited");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid event update attempt");
            return View();
        }
        private async Task<EventViewModel> GetEventByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Events/{id}");
            var eventVM = new EventViewModel();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                eventVM = JsonConvert.DeserializeObject<EventViewModel>(result);
            }
            return eventVM;
        }
    }
}
