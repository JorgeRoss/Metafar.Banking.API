using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Transactions.Commands.CreateTransactionCommand
{
    public sealed record CreateTransactionCommand : IRequest<Response<bool>>
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
