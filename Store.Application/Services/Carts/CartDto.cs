namespace Store.Application.Services.Carts
{
    public class CartDto
    {
        public long CartId { get; set; }
        public List<CartProductDto> ProductDtos { get; set; }
        public int TotalPrice { get; set; }

    }
}
