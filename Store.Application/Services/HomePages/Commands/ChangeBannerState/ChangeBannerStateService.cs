using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.ChangeBannerState
{
    public class ChangeBannerStateService: IChangeBannerStateService
    {
        private readonly IDataBaseContext _context;
        public ChangeBannerStateService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int bannerId)
        {
            var banner = _context.Banners.Find(bannerId);
            if (banner!=null)
            {
                banner.DisplayOnPage = !banner.DisplayOnPage;
                _context.SaveChanges();
                string state = banner.DisplayOnPage ? "داده میشود" : "داده نمیشود";
                return new ResultDto { IsSuccess = true, Message = $"بنر در سایت نمایش {state}" };
            }
            return new ResultDtoError();
        }
    }
}
