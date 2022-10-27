using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.HomePages.Commands.DeleteBanner
{
    public class DeleteBannerService: IDeleteBannerService
    {
        private readonly IDataBaseContext _context;
        public DeleteBannerService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int bannerId)
        {
            var banner = _context.Banners.Find(bannerId);
            if (banner != null)
            {
                banner.IsRemoved = true;
                banner.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true ,Message="با موفقیت حذف شد !"};
            }
            else
                return new ResultDto { Message = "پیدا نشد !" };
        }
    }
}
