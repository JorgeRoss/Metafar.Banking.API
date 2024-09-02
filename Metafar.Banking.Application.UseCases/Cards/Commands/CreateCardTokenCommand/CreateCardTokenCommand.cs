using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Cards.Commands.CreateCardTokenCommand
{
    public sealed record CreateCardTokenCommand : IRequest<Response<CardTokenDto>>
    {
        public string cardNumber { get; set; }
        public string pin { get; set; }
    }
}
