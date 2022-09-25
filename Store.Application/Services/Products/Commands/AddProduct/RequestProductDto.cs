using Microsoft.AspNetCore.Http;

namespace Store.Application.Services.Products.Commands.AddProduct
{
    public class RequestProductDto
    {
        public string ProductTitle { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public long CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<RequestFeatureDto> ProductFeatures { get; set; }
    }
   
}
