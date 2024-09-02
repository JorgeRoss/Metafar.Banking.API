using Metafar.Banking.Cross.Common;

namespace Metafar.Banking.Application.UseCases.Common.Exceptions
{
    public class ValidationExceptionCustom : Exception
    {
        public ValidationExceptionCustom() : base("One or more validation failures")
        {
            Errors = new List<BaseError>();
        }

        public ValidationExceptionCustom(IEnumerable<BaseError>? errors) : this()
        {
            Errors = errors;
        }

        public IEnumerable<BaseError>? Errors { get; }
    }
}
