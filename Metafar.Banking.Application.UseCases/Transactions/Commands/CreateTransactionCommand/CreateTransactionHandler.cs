using AutoMapper;
using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.DTO.Enums;
using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Cross.Common;
using Metafar.Banking.Domain.Entities;
using System.Text.Json;

namespace Metafar.Banking.Application.UseCases.Transactions.Commands.CreateTransactionCommand
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            var card = await _unitOfWork.Cards.GetByCardNumberAsync(request.CardNumber);

            if (card == null || card.IsBlocked)
            {
                response.IsSuccess = true;
                response.Message = "The card does not exist or is blocked";
                return response;
            }

            var account = await _unitOfWork.Accounts.GetAsync(card.UserId);

            if (account == null || (account.Balance < request.Amount))
            {
                response.IsSuccess = true;
                response.Message = "Insufficient balance";
                return response;
            }

            account.Balance -= request.Amount;
            account.LastExtractionDate = DateTime.Now;

            var transaction = new Transaction
            {
                CardId = card.Id,
                Amount = request.Amount,
                TransactionTypeId = (int)TransactionTypesDto.Withdraw,
                TransactionDate = DateTime.Now
            };

            await _unitOfWork.Transactions.InsertAsync(transaction);
            
            response.Data = await _unitOfWork.Save(cancellationToken) > 0;
            response.IsSuccess = true;
            response.Message = "Successful Operation!!!";
            
            return response;
        }

    }
}
