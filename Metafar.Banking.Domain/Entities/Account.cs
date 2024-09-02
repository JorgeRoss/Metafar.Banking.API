namespace Metafar.Banking.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastExtractionDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
