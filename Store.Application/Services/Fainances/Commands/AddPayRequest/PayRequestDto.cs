namespace Store.Application.Services.Fainances.Commands.AddPayRequest
{
    public class PayRequestDto
    {
        public long UserId { get; set; }
        public long CartId { get; set; }
        public int TransportPrice { get; set; } = 0;
    }
}
