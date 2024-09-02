using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Transactions.Queries
{
    public sealed record GetTransactionQuery : IRequest<PaginatedResponse<TransactionDto>>
    {
        public string CardNumber { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
