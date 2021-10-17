using EventsApp.Api.Models.DTOs;
using EventsApp.Api.Models.Requests;
using EventsApp.Data;
using EventsApp.Domain.POCO;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsApp.Api.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AppConfigsController : Controller
    {
        private readonly IAppConfigRepository _repo;
        public AppConfigsController(IAppConfigRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns all AppConfigs
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result.Adapt<List<AppConfigDTO>>());
        }

        /// <summary>
        /// Returns Config
        /// </summary>
        /// <param name="setting">Setting (Index, PK)</param>
        [HttpGet("{setting}")]
        public async Task<IActionResult> Get(string setting)
        {
            var result = await _repo.GetAsync(setting);
            return Ok(result.Adapt<AppConfigDTO>());
        }

        /// <summary>
        /// Create AppConfig
        /// </summary>
        /// <param name="configRequest">Config model</param>
        [HttpPost]
        public async Task<IActionResult> Post(AppConfigRequst configRequest)
        {
            var config = configRequest.Adapt<AppConfig>();
            var id = await _repo.CreateAsync(config);
            return id != null ?  Ok(id) :  NotFound();
        }

        /// <summary>
        /// Update AppConfig
        /// </summary>
        /// <param name="configRequest">Config model</param>
        [HttpPut]
        public async Task<IActionResult> Put(AppConfigRequst configRequest)
        {
            var config = configRequest.Adapt<AppConfig>();
            await _repo.UpdateAsync(config);
            return Ok();
        }
    }
}
