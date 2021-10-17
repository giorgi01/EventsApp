using EventsApp.Data;
using EventsApp.Domain;
using EventsApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.DataEF
{
    public class AppConfigRepository : IAppConfigRepository
    {
        readonly IBaseRepository<AppConfig> _repository;

        public AppConfigRepository(IBaseRepository<AppConfig> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(AppConfig config)
        {
            await _repository.AddAsync(config);
            return config.Setting;
        }

        public Task DeleteAsync(string setting)
        {
            return _repository.RemoveAsync(setting);
        }

        public async Task<bool> Exists(string setting)
        {
            return await _repository.AnyAsync(x => x.Setting == setting);
        }

        public async Task<List<AppConfig>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AppConfig> GetAsync(string setting)
        {
            return await _repository.Table.FirstOrDefaultAsync(x => x.Setting == setting);
        }

        public Task UpdateAsync(AppConfig config)
        {
            return _repository.UpdateAsync(config);
        }
    }
}
