using AutoMapper;
using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Cards.Commands.CreateCardTokenCommand
{
    public class CreateCardTokenHandler : IRequestHandler<CreateCardTokenCommand, Response<CardTokenDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCardTokenHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CardTokenDto>> Handle(CreateCardTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CardTokenDto>();
            var card = await _unitOfWork.Cards.GetByCardNumberAsync(request.cardNumber);

            if (card is null)
            {
                response.IsSuccess = true;
                response.Message = "The card does not exist";
                return response;
            }

            if (card.IsBlocked)
            {
                throw new UnauthorizedAccessException("Card is blocked.");
            }
            else if (!VerifyPinHash(request.pin, card.Pin))
            {
                card.FailedCodeAttempts++;
                if (card.FailedCodeAttempts >= 4)
                {
                    card.IsBlocked = true;
                }
                await _unitOfWork.Cards.UpdateAsync(card);
                await _unitOfWork.Save(cancellationToken);

                throw new UnauthorizedAccessException("Invalid card number or PIN.");
            }

            card.FailedCodeAttempts = 0;
            await _unitOfWork.Cards.UpdateAsync(card);
            await _unitOfWork.Save(cancellationToken);

            response.Data = _mapper.Map<CardTokenDto>(card);
            response.IsSuccess = true;
            response.Message = "Successful Operation!!!";

            return response;
        }

        private bool VerifyPinHash(string pin, string hash)
        {
            return pin == hash;
        }
    }
}
