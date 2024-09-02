using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Domain.Entities;
using Metafar.Banking.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Banking.Persistence.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public AccountsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }       

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAsync(int userId)
        {
            var entity = await _applicationDbContext.Accounts.Include(a => a.User)
                .SingleOrDefaultAsync(x => x.UserId.Equals(userId));

            return entity;
        }

        public Task<bool> InsertAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
