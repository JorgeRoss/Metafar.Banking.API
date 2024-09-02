namespace Metafar.Banking.Application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICardsRepository Cards { get; }
        ITransactionsRepository Transactions { get; }
        IAccountsRepository Accounts { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
