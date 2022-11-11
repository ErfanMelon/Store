using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUserDetailSite
{
    public interface IGetUserDetailSiteService
    {
        ResultDto<UserDetailSiteDto> Execute(long userId);
    }
    public class GetUserDetailSiteService: IGetUserDetailSiteService
    {
        private readonly IDataBaseContext _context;
        public GetUserDetailSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserDetailSiteDto> Execute(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user!=null)
            {
                return new ResultDto<UserDetailSiteDto>
                {
                    Data=new UserDetailSiteDto
                    {
                        Address=user.Address,
                        Email=user.Email,
                        Name=user.UserFullName,
                        PhoneNumber=user.PhoneNumber,
                        ZipCode=user.ZipCode
                    },
                    IsSuccess=true
                };
            }
            return new ResultDto<UserDetailSiteDto> { Message = "کاربر پیدا نشد !" };
        }
    }
    public class UserDetailSiteDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ZipCode { get; set; }
    }
}
