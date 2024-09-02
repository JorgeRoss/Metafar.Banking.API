namespace Metafar.Banking.Application.DTO.Entities
{
    public class CardDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public int FailedCodeAttempts { get; set; }
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
    }
}
