using EventsApp.Domain.POCO;
using EventsApp.Mvc.Models;
using EventsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EventsApp.Mvc.Helpers;
using System.Collections;
using X.PagedList;

namespace EventsApp.AdminPanel.Controllers
{
    [Authorize(Policy = "AtLeastAdmin")]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        #region Private Members
        private readonly HttpClient _client;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region ctor
        public EventsController(EventsAPI api, UserManager<ApplicationUser> userManager)
        {
            _client = api.Client;
            _userManager = userManager;
        }
        #endregion

        [HttpGet]
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

        [HttpGet("[action]")]
        public async Task<IActionResult> UnpublishedEvents(int? page)
        {
            var pageNumber = page ?? 1;
            var response = await _client.GetAsync("Events/GetUnpublishedEvents");
            var indexVM = new List<EventViewModel>();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                indexVM = JsonConvert.DeserializeObject<List<EventViewModel>>(result);
            }
            return View(indexVM.ToPagedList(pageNumber, 3));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArchivedEvents(int? page)
        {
            var pageNumber = page ?? 1;
            var response = await _client.GetAsync("Events/GetArchivedEvents");
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
                eventVM.IsActive = true;
                var response = await _client.PostAsJsonAsync("Events", eventVM);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
            }
            ModelState.AddModelError(string.Empty, "Invalid event creation attempt");
            return View();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var eventVM = await GetEventVMAsync(id);
            return eventVM != null ? View(eventVM) : RedirectToAction("Error", "Home");
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventVM = await GetEventVMAsync(id);
            return eventVM != null ? View(eventVM) : RedirectToAction("Error", "Home");
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Edit(EventViewModel eventVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PutAsJsonAsync("Events", eventVM);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else if ((int)response.StatusCode == 400)
                    await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
                else
                    return RedirectToAction("Error", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid update attempt");
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Publish([FromBody] EventViewModel eventVM)
        {
            eventVM.IsActive = true;
            var response = await _client.PutAsJsonAsync("Events", eventVM);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else if ((int)response.StatusCode == 400)
                await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
            else
                return RedirectToAction("Error", "Home");
            return View();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"Events/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index)); 
            else
                return RedirectToAction("Error", "Home");
        }

        private async Task<EventViewModel> GetEventVMAsync(int id)
        {
            var response = await _client.GetAsync($"Events/{id}");
            var eventVM = new EventViewModel();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                eventVM = JsonConvert.DeserializeObject<EventViewModel>(result);
            }
            else
            {
                return null;
            }
            return eventVM;
        }
    }
}
