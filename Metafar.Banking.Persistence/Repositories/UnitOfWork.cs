using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Domain.Entities;
using Metafar.Banking.Persistence.Contexts;

namespace Metafar.Banking.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICardsRepository Cards { get; }
        public ITransactionsRepository Transactions { get; }
        public IAccountsRepository Accounts { get; }

        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ICardsRepository cards, ITransactionsRepository transactions, IAccountsRepository account, ApplicationDbContext applicationDbContext)
        {
            Cards = cards;
            Transactions = transactions;
            Accounts = account;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
