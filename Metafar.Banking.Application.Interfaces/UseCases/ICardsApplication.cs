using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.UseCases.Users
{
    public interface ICardsApplication
    {
        Task<Response<CardTokenDto>> Authenticate(string cardNumber, string pin);
    }
}
