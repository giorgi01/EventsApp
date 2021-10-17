using EventsApp.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.Data
{
    public interface IUserRepository
    {
        Task<string> CreateAsync(ApiUser user);
        Task DeleteAsync(string id);
        Task<bool> Exists(string id);
        Task<List<ApiUser>> GetAllAsync();
        Task<ApiUser> GetAsync(string id);
        Task UpdateAsync(ApiUser user);

    }
}
