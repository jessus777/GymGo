using FluentResults;
using GymGo.Application.Contracts;
using GymGo.Application.Dtos;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.Users.Quries.GetUserList
{
    public sealed class GetUserListQueryHandler
        : ApplicationRequestHandler<GetUserListQuery, List<UserDto>>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public GetUserListQueryHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override async Task<Result<List<UserDto>>> HandleAsync(GetUserListQuery request, CancellationToken cancellationToken)
        {
            using var unitOfWork = _unitOfWorkFactory.CreateIdentity();
            var users = await unitOfWork.UserRepositoryAsync.GetAllAsync(cancellationToken);
            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            });
            return Ok([.. userDtos]);
        }
    }
}
