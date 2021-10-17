using EventsApp.Domain;
using EventsApp.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsApp.Data
{
    public interface IAppConfigRepository
    {
        Task<List<AppConfig>> GetAllAsync();
        Task<AppConfig> GetAsync(string setting);
        Task<string> CreateAsync(AppConfig config);
        Task<bool> Exists(string setting);
        Task UpdateAsync(AppConfig config);
        Task DeleteAsync(string setting);
    }
}
