using Metafar.Banking.Application.DTO.Enums;

namespace Metafar.Banking.Application.DTO.Entities
{
    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastExtractionDate { get; set; }
        public string UserName { get; set; }
    }
}
