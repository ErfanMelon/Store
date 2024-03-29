﻿using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;

namespace Store.Application.Services.Fainances.Commands.EditPayRequset
{
    public class EditRequsetPayService: IEditRequsetPayService
    {
        private readonly IDataBaseContext _context;
        public EditRequsetPayService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(EditRequestPayDto requset)
        {
            var requsetPay = _context.RequestPays.Where(r=>r.PayId== requset.RequsetPayId)
                .Include(r=>r.Cart)
                .FirstOrDefault();
            if (requsetPay!=null)
            {
                requsetPay.IsPay = requset.IsPay;
                requsetPay.RefId = requset.RefId;
                requsetPay.UpdateTime = DateTime.Now;
                requsetPay.Authority = requset.Authority;
                if (requset.IsPay)
                {
                    requsetPay.Cart.CurrentCart = false;
                    requsetPay.PayDate = DateTime.Now;
                }
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true };
            }
            return new ResultDto
            {
                Message = "پیدا نشد !"
            };
        }
    }
}
