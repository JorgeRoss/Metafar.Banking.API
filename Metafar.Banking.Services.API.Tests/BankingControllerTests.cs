using MediatR;
using Metafar.Banking.API.Controllers.v1;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.UseCases.Accounts.Queries.GetAccountQuery;
using Metafar.Banking.Application.UseCases.Transactions.Commands.CreateTransactionCommand;
using Metafar.Banking.Application.UseCases.Transactions.Queries;
using Metafar.Banking.Cross.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Metafar.Banking.Services.API.Tests
{
    [TestFixture]
    public class BankingControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private BankingController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BankingController(_mediatorMock.Object);
        }

        [Test]
        public async Task Balance_ReturnsOkResult_WhenAccountExists()
        {
            // Arrange
            var cardNumber = "1234567890";
            var response = new Response<AccountDto>
            {
                Data = new AccountDto { Balance = 1000 }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Balance(cardNumber);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public async Task Balance_ReturnsNotFoundResult_WhenAccountDoesNotExist()
        {
            // Arrange
            var cardNumber = "1234567890";
            var response = new Response<AccountDto>
            {
                Data = null,
                Message = "Account not found"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAccountQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Balance(cardNumber);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual(response.Message, notFoundResult.Value);
        }

        [Test]
        public async Task Withdraw_ReturnsOkResult_WhenTransactionIsSuccessful()
        {
            // Arrange
            var command = new CreateTransactionCommand { Amount = 100, CardNumber = "1234567890" };
            var response = new Response<bool>
            {
                Data = true
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Withdraw(command);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public async Task Withdraw_ReturnsNotFoundResult_WhenTransactionFails()
        {
            // Arrange
            var command = new CreateTransactionCommand { Amount = 100, CardNumber = "1234567890" };
            var response = new Response<bool>
            {
                Data = false,
                Message = "Insufficient funds"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Withdraw(command);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual(response.Message, notFoundResult.Value);
        }

        [Test]
        public async Task Operations_ReturnsOkResult_WhenTransactionsExist()
        {
            // Arrange
            var cardNumber = "1234567890";
            var pageNumber = 1;
            var pageSize = 10;
            var response = new Response<List<TransactionDto>>();
            response.Data = new List<TransactionDto> { new TransactionDto { Amount = 100 } };

            var paginatedresponse = new PaginatedResponse<TransactionDto>
            {
                Response = response,
                TotalCount = 1,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = 1
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedresponse);

            // Act
            var result = await _controller.Operations(cardNumber, pageNumber, pageSize);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(paginatedresponse, okResult.Value);
        }

        [Test]
        public async Task Operations_ReturnsNotFoundResult_WhenTransactionsDoNotExist()
        {
            // Arrange
            var cardNumber = "1234567890";
            var pageNumber = 1;
            var pageSize = 10;
            var response = new Response<List<TransactionDto>>();

            var paginatedresponse = new PaginatedResponse<TransactionDto>
            {
                Response = response,
                TotalCount = 0,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = 0
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedresponse);

            // Act
            var result = await _controller.Operations(cardNumber, pageNumber, pageSize);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}