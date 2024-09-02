using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Accounts.Queries.GetAccountQuery
{
    public sealed record GetAccountQuery : IRequest<Response<AccountDto>>
    {
        public string CardNumber { get; set; }
    }
}
