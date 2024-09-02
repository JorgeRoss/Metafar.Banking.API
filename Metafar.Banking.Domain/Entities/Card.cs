namespace Metafar.Banking.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }        
        public int FailedCodeAttempts { get; set; }
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
