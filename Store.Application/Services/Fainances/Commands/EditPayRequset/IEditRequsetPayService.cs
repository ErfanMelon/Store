using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Fainances.Commands.EditPayRequset
{
    public interface IEditRequsetPayService
    {
        ResultDto Execute(EditRequestPayDto requset);
    }
}
