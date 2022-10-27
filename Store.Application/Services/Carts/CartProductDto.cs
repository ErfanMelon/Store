namespace Store.Application.Services.Carts
{
    public class CartProductDto
    {
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public short Count { get; set; }
        public string ProductImg { get; set; }
    }
}
