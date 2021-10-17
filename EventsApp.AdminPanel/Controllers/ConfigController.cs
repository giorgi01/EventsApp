using EventsApp.Mvc.Helpers;
using EventsApp.Mvc.Models;
using EventsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EventsApp.AdminPanel.Controllers
{
    [Authorize(Policy = "AtLeastAdmin")]
    public class ConfigController : Controller
    {
        private readonly HttpClient _client;
        public ConfigController (EventsAPI api)
        {
            _client = api.Client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("AppConfigs");
            var indexVM = new ConfigIndexViewModel();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var configs = JsonConvert.DeserializeObject<List<ConfigViewModel>>(result);
                indexVM.Configs = configs;
            }
            return View(indexVM);
        }

        [HttpGet("[controller]/[action]/{setting?}")]
        public async Task<IActionResult> Details(string setting)
        {
            var configVM = await GetConfigBySettingAsync(setting);
            return configVM != null ? View(configVM) : RedirectToAction("Error", "Home");
        }

        [HttpGet("[controller]/[action]/{setting?}")]
        public async Task<IActionResult> Edit(string setting)
        {
            var configVM = await GetConfigBySettingAsync(setting);
            return configVM != null ? View(configVM) : RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ConfigViewModel configVM)
        {
            var response = await _client.PutAsJsonAsync("AppConfigs", configVM);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else if ((int)response.StatusCode == 400)
            {
                await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
                return View(configVM);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfigViewModel configVM)
        {
            var response = await _client.PostAsJsonAsync("AppConfigs", configVM);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else if ((int)response.StatusCode == 400)
            {
                await ControllerHelper.ModelStateAddErrorsAsync(ModelState, response);
                return View(configVM);
            }
            else
                return RedirectToAction("Error", "Home");
        }

        private async Task<ConfigViewModel> GetConfigBySettingAsync(string setting)
        {
            var response = await _client.GetAsync($"AppConfigs/{setting}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ConfigViewModel>(result);
            }
            return null;
        }
    }
}
