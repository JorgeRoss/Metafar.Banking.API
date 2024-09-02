using Metafar.Banking.Application.DTO.Enums;

namespace Metafar.Banking.Application.DTO.Entities
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public CardDto Card { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public TransactionTypesDto TransactionType { get; set; }
    }
}
