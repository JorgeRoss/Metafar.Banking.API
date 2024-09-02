namespace Metafar.Banking.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
