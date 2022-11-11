namespace Store.Application.Services.Carts
{
    public class CartDto
    {
        public long CartId { get; set; }
        public List<CartProductDto> ProductDtos { get; set; }
        public int TotalPrice { get; set; }
        public string? Address { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
