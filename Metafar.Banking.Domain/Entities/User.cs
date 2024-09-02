namespace Metafar.Banking.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
