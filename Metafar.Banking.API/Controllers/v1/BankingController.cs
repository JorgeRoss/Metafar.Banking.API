using Asp.Versioning;
using MediatR;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.UseCases.Accounts.Queries.GetAccountQuery;
using Metafar.Banking.Application.UseCases.Transactions.Commands.CreateTransactionCommand;
using Metafar.Banking.Application.UseCases.Transactions.Queries;
using Metafar.Banking.Cross.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Metafar.Banking.API.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Banking")]
    public class BankingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankingController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("Balance/{cardNumber}")]
        [SwaggerOperation(
            Summary = "Balance Account by card number",
            Description = "This endpoint will return balance",
            OperationId = "Get",
            Tags = new string[] { "Get" })]
        [SwaggerResponse(200, "Successful", typeof(Response<AccountDto>))]
        [SwaggerResponse(404, "Notfound")]
        public async Task<IActionResult> Balance([FromRoute] string cardNumber)
        {
            var response = await _mediator.Send(new GetAccountQuery() { CardNumber = cardNumber });

            if (response.Data == null)
            {
                return NotFound(response.Message);
            }

            return Ok(response);

        }

        [HttpPost("Withdraw")]
        [SwaggerOperation(
            Summary = "Withdraw operation",
            Description = "This endpoint will save new withdraw",
            OperationId = "Withdraw",
            Tags = new string[] { "Create" })]
        [SwaggerResponse(200, "Successful", typeof(Response<bool>))]
        [SwaggerResponse(404, "Notfound")]
        public async Task<IActionResult> Withdraw([FromBody] CreateTransactionCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Data)
            {
                return NotFound(response.Message);
            }

            return Ok(response);

        }

        [HttpGet("Operations/{cardNumber}")]
        [SwaggerOperation(
            Summary = "Transactions by card number",
            Description = "This endpoint will return transactions with pagination",
            OperationId = "Get",
            Tags = new string[] { "Get" })]
        [SwaggerResponse(200, "Successful", typeof(PaginatedResponse<TransactionDto>))]
        [SwaggerResponse(404, "Notfound")]
        public async Task<IActionResult> Operations(string cardNumber, int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetTransactionQuery
            {
                CardNumber = cardNumber,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var paginateResponse = await _mediator.Send(query);

            if (paginateResponse.Response.Data == null)
            {
                return NotFound(paginateResponse.Response.Message);
            }

            return Ok(paginateResponse);

        }

    }
}
