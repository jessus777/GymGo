using GymGo.Application.Dtos;
using GymGo.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Application.Features.Users.Quries.GetUserList
{
    public sealed class GetUserListQuery
        : ApplicationRequest<List<UserDto>>
    {
    }
}
