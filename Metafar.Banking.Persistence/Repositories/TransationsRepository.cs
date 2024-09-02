using Azure.Core;
using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Domain.Entities;
using Metafar.Banking.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Metafar.Banking.Persistence.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public TransactionsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }       

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Transaction entity)
        {
            _applicationDbContext.Add(entity);
            return await Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetAllWithPaginationByCardNumberAsync(int cardId, int pageNumber = 1, int pageSize = 10)
        {
            var result = _applicationDbContext.Transactions.Include(t => t.TransactionType)
                        .Where(t => t.CardId == cardId)
                        .OrderByDescending(t => t.TransactionDate);


             return result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> CountAsync(int cardId)
        {
            return _applicationDbContext.Transactions.Count(t => t.Card.Id == cardId); ;
        }
    }
}
