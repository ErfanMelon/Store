namespace Store.Application.Services.Fainances.Commands.AddPayRequest
{
    public class ResultAddPayRequest
    {
        public int TotalPrice { get; set; }
        public Guid RequestPayId { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
