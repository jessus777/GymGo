using AutoMapper;
using GymGo.Application.Contracts.Persistence;
using GymGo.Application.Dtos;
using MediatR;

namespace GymGo.Application.Features.Clients.Queries.GetAllClients
{
    public sealed class GetAllClientsQueryHandler
    //: IRequestHandler<GetAllClientsQuery, List<ClientDto>>
    {
        //    private readonly IUnitOfWork _unitOfWork;
        //    private readonly IMapper _mapper;

        //    public GetAllClientsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        //    {
        //        _mapper = mapper;
        //        _unitOfWork = unitOfWork;
        //    }

        //    public async Task<List<ClientDto>> Handle(
        //        GetAllClientsQuery request, CancellationToken cancellationToken
        //        )
        //    {
        //        var unitOfWork = _unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        //        var clienteList = (await unitOfWork.ClientRepositoryAsync.GetAllAsync()).OrderBy(x => x.Name);
        //        return _mapper.Map<List<ClientDto>>(clienteList);
        //    }
        //}
    }
}
