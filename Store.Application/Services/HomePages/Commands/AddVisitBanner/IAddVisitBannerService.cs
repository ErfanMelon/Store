using Store.Application.Interfaces.Context;

namespace Store.Application.Services.HomePages.Commands.AddVisitBanner
{
    public interface IAddVisitBannerService
    {
        void Execute(int bannerId);
    }
    public class AddVisitBannerService: IAddVisitBannerService
    {
        private readonly IDataBaseContext _context;
        public AddVisitBannerService(IDataBaseContext context)
        {
            _context = context;
        }

        public void Execute(int bannerId)
        {
            _context.Banners.FirstOrDefault(b => b.BannerId == bannerId).Clicks++;
            _context.SaveChanges();
        }
    }
}
