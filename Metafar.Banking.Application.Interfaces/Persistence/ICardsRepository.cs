using Metafar.Banking.Domain.Entities;

namespace Metafar.Banking.Application.Interfaces.Persistence
{
    public interface ICardsRepository : IGenericRepository<Card>
    {
        Task<Card> GetByCardNumberAsync(string cardNumber);
    }
}
