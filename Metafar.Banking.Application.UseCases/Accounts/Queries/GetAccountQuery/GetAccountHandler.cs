using AutoMapper;
using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Accounts.Queries.GetAccountQuery
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, Response<AccountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<AccountDto>();

            var card = await _unitOfWork.Cards.GetByCardNumberAsync(request.CardNumber);

            if (card == null || card.IsBlocked)
            {
                response.IsSuccess = true;
                response.Message = "The card does not exist or is blocked";
                return response;
            }

            var account = await _unitOfWork.Accounts.GetAsync(card.UserId);

            response.Data = _mapper.Map<AccountDto>(account);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Successful!!!";
            }

            return response;
        }
    }
}
