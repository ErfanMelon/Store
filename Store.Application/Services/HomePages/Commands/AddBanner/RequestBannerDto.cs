using Microsoft.AspNetCore.Http;
using Store.Domain.Entities.HomePages;

namespace Store.Application.Services.HomePages.Commands.AddBanner
{
    public class RequestBannerDto
    {
        public bool DisplayOnSite { get; set; }
        public IFormFile Image { get; set; }
        public string? Link { get; set; }
        public BannerLocation BannerLocation { get; set; }
    }
}
