using Metafar.Banking.Domain.Entities;

namespace Metafar.Banking.Application.Interfaces.Persistence
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllWithPaginationByCardNumberAsync(int cardId, int pageNumber = 1, int pageSize = 10);
        Task<int> CountAsync(int cardId);
    }
}
