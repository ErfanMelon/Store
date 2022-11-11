using Store.Application.Interfaces.Context;
using Store.Application.Validations.User;
using Store.Common.Dto;

namespace Store.Application.Services.Users.Commands.EditUserSite
{
    public interface IEditUserSiteService
    {
        ResultDto Execute(EditUserSiteDto request);
    }

    public class EditUserSiteService: IEditUserSiteService
    {
        private readonly IDataBaseContext _context;
        public EditUserSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(EditUserSiteDto request)
        {
            EditUserSiteValidation validations = new EditUserSiteValidation();
           var isrequestvalid= validations.Validate(request);
            if (!isrequestvalid.IsValid)
            {
                return new ResultDto { Message = isrequestvalid.Errors[0].ErrorMessage };
            }
            var user = _context.Users.Find(request.UserId);
            if (user!=null)
            {
                user.Address = request.Address;
                user.PhoneNumber = request.PhoneNumber;
                user.ZipCode = request.ZipCode;
                user.UserFullName = request.FullName;
                user.UpdateTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "اطلاعات با موفقیت بروز شد !" };
            }
            return new ResultDto { Message = "کاربر پیدا نشد !" };
        }
    }
    public class EditUserSiteDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ZipCode { get; set; }
    }
}
