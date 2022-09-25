using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.ChangeUserState
{
    public interface IChangeUserStateService
    {
        ResultDto<long> Execute(long userId);
    }
}
