using AutoMapper;
using FluentResults;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Application.Exceptions;
using GymGo.Application.Requests;

namespace GymGo.Application.Features.MembershipTypes.Commands.DeleteMembershipType
{
    public sealed class DeleteMembershipTypeCommandHandler
        : ApplicationRequestHandler<DeleteMembershipTypeCommand, MembershipTypeDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public DeleteMembershipTypeCommandHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override async Task<Result<MembershipTypeDto>> HandleAsync(
            DeleteMembershipTypeCommand request,
            CancellationToken cancellationToken
            )
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var membershipType = await unitOfWork
                .MembershipTypeUnitOfWork
                .MembershipTypeRepositoryAsync
                .GetByIdAsync(request.Id);

            if (membershipType is null)
            {
                return DomainError.EntityNotFound(request.Id);
            }

            if (membershipType.IsActive)
            {
                return DomainError.EntityConflict(request.Id, "No se puede eliminar.");
            }
            membershipType.Delete();

            await unitOfWork.MembershipTypeUnitOfWork
                .MembershipTypeRepositoryAsync
                .UpdateAsync(membershipType);

            await unitOfWork.CommitAsync(cancellationToken);

            var membershipTypeDto = _mapper.Map<MembershipTypeDto>(membershipType);

            return Ok(membershipTypeDto);
        }
    }
}
