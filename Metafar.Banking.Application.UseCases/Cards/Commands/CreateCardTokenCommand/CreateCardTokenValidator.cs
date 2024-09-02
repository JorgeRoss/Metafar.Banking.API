using FluentValidation;

namespace Metafar.Banking.Application.UseCases.Cards.Commands.CreateCardTokenCommand
{
    public class CreateCardTokenValidator : AbstractValidator<CreateCardTokenCommand>
    {
        public CreateCardTokenValidator()
        {
            RuleFor(u => u.cardNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(16);

            RuleFor(u => u.pin)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}
