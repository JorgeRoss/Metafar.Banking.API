namespace Metafar.Banking.Application.DTO.Entities
{
    public class CardTokenDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int FailedCodeAttempts { get; set; }
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
