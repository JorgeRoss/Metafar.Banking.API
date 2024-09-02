using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Domain.Entities;
using Metafar.Banking.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Banking.Persistence.Repositories
{
    public class CardsRepository : ICardsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public CardsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Card> GetByCardNumberAsync(string cardNumber)
        {
            var entity = await _applicationDbContext.Set<Card>().AsNoTracking().Include(c => c.User)
                .SingleOrDefaultAsync(x => x.CardNumber.Equals(cardNumber));

            return entity;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Card>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Card entity)
        {
            throw new NotImplementedException();
        }
                
        public async Task<bool> UpdateAsync(Card entity)
        {
            _applicationDbContext.Update(entity);
            return await Task.FromResult(true);
        }
    }
}
