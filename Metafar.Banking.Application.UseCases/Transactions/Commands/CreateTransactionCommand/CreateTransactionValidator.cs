using FluentValidation;

namespace Metafar.Banking.Application.UseCases.Transactions.Commands.CreateTransactionCommand
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
