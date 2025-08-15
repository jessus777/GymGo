using AutoMapper;
using FluentResults;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Application.Exceptions;
using GymGo.Application.Requests;
using GymGo.Domain.Entities;

namespace GymGo.Application.Features.MembershipTypes.Commands.CreateMembershipType
{
    public sealed class CreateMembershipTypeCommandHandler
        : ApplicationRequestHandler<CreateMembershipTypeCommand, MembershipTypeDto>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public CreateMembershipTypeCommandHandler(
            IUnitOfWorkFactory unitOfWorkFactory,
            IMapper mapper
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        protected override async Task<Result<MembershipTypeDto>> HandleAsync(
            CreateMembershipTypeCommand request,
            CancellationToken cancellationToken
            )
        {
            using var unitOfWork = _unitOfWorkFactory.Create();

            var existingMembershipType = await unitOfWork.MembershipTypeRepositoryAsync
                .GetByNameAsync(request.Name, cancellationToken);

            if (existingMembershipType is not null)
            {
                return DomainError.DuplicateEntity(existingMembershipType);  
            }

            var membershipType = MembershipType.Create(
                request.Name,
                request.Description,
                request.Price,
                request.DurationInDays
            );

            await unitOfWork.MembershipTypeRepositoryAsync.AddAsync(membershipType);
            await unitOfWork.CommitAsync(cancellationToken);
            var membershipTypeDto = _mapper.Map<MembershipTypeDto>(membershipType);
            return Ok(membershipTypeDto);
        }
    }
}
