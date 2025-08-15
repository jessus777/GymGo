using AutoMapper;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using GymGo.Domain.Entities;
using MediatR;

namespace GymGo.Application.Features.Clients.Queries.GetClientDetail
{
    public sealed class GetClientDetailQueryHandler
        //: IRequestHandler<GetClientDetailQuery, ClientDetailDto>
    {
        //IUnitOfWorkFactory _unitOfWorkFactory;
        //readonly IMapper _mapper;

        //public GetClientDetailQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper)
        //{
        //    _unitOfWorkFactory = unitOfWorkFactory;
        //    _mapper = mapper;
        //}

        //public async Task<ClientDetailDto> Handle(
        //    GetClientDetailQuery request, 
        //    CancellationToken cancellationToken
        //    )
        //{
        //    //var client = await _clientRepository.GetByIdAsync(request.ClientId);
        //    //var clientDetailDto = _mapper.Map<ClientDetailDto>(client);
        //    //return clientDetailDto;
        //    return OK
        //}
    }
}
