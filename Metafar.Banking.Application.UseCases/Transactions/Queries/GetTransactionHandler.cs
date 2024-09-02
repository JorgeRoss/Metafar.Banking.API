using AutoMapper;
using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.Interfaces.Persistence;
using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Transactions.Queries
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, PaginatedResponse<TransactionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var paginateResponse = new PaginatedResponse<TransactionDto>();
            var response = new Response<List<TransactionDto>>();
            var card = await _unitOfWork.Cards.GetByCardNumberAsync(request.CardNumber);

            if (card == null || card.IsBlocked)
            {
                response.IsSuccess = true;
                response.Message = "The card does not exist or is blocked";
                paginateResponse.Response = response;
                return paginateResponse;
            }

            var transactions = await _unitOfWork.Transactions.GetAllWithPaginationByCardNumberAsync(card.Id, request.PageNumber, request.PageSize);
            var totalTransactions = await _unitOfWork.Transactions.CountAsync(card.Id);
            var totalPages = (int)Math.Ceiling(totalTransactions / (double)request.PageSize);

            response.Data = _mapper.Map<List<TransactionDto>>(transactions);

            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Successful!!!";
            }

            paginateResponse.Response = response;
            paginateResponse.TotalPages = totalPages;
            paginateResponse.CurrentPage = request.PageNumber;
            paginateResponse.TotalCount = totalTransactions;
            paginateResponse.PageSize = request.PageSize;

            return paginateResponse;
        }
    }
}
