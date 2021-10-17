using EventsApp.Data;
using EventsApp.Domain;
using EventsApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.DataEF
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository<ApiUser> _repository;
        public UserRepository(IBaseRepository<ApiUser> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(ApiUser user)
        {
            await _repository.AddAsync(user);
            return user.Id;
        }

        public Task DeleteAsync(string id)
        {
            return _repository.RemoveAsync(id);
        }

        public async Task<bool> Exists(string id)
        {
            return await _repository.AnyAsync(x => x.Id == id);
        }

        public async Task<List<ApiUser>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ApiUser> GetAsync(string id)
        {
            return await _repository.Table.Include(x => x.Events).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(ApiUser user)
        {
            return _repository.UpdateAsync(user);
        }
    }
}
