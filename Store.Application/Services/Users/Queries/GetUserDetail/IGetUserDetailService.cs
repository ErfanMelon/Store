using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Orders;

namespace Store.Application.Services.Users.Queries.GetUserDetail
{
    public interface IGetUserDetailService
    {
        ResultDto<UserDetailDto> Execute(long userId);
    }
    public class GetUserDetailService: IGetUserDetailService
    {
        private readonly IDataBaseContext _context;
        public GetUserDetailService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserDetailDto> Execute(long userId)
        {
            var user = _context.Users.Where(u => u.UserId == userId)
                .Include(u => u.Role)
                .Include(u => u.Orders)
                .ThenInclude(o=>o.OrderDetails)
                .FirstOrDefault();
            if (user!=null)
            {
                UserDetailDto userDetail = new UserDetailDto
                {
                    Address = user.Address,
                    BoughtCount = user.Orders != null ? user.Orders // product count that user doesn't cancell them
                    .Where(o=>o.OrderState!=OrderState.Cancelled)
                    .Sum(o => o.OrderDetails.Sum(d => d.Count)):0,
                    IsActive = user.IsActive,
                    JoinDate = user.InsertTime,
                    Role = user.Role.RoleName,
                    TotalPaid = user.Orders!=null? user.Orders // total price user spend except refund(for cancelled orders)
                    .Where(o => o.OrderState != OrderState.Cancelled)
                    .Sum(o => o.OrderDetails.Sum(d => d.Amount) - o.OrderRefund):0,
                    UserId=user.UserId,
                    UserName=user.UserFullName,
                    Email=user.Email,
                    PhoneNumber=user.PhoneNumber,
                    ZipCode=user.ZipCode
                };
                return new ResultDto<UserDetailDto> 
                {
                    Data=userDetail,
                    IsSuccess=true
                };
            }
            return new ResultDto<UserDetailDto> {Data=new UserDetailDto(), Message = "کاربری پیدا نشد !" };
        }
    }
    public class UserDetailDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoinDate { get; set; }
        public int BoughtCount { get; set; } = 0;
        public int TotalPaid { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ZipCode { get; set; }
    }
}
