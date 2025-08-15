using GymGo.Api.Extensions;
using GymGo.Application.Features.MembershipTypes.Commands.CreateMembershipType;
using GymGo.Application.Features.MembershipTypes.Commands.DeleteMembershipType;
using GymGo.Application.Features.MembershipTypes.Queries.GetAllMembershipType;
using GymGo.Application.Features.MembershipTypes.Queries.GetDetailMembershipType;
using GymGo.Application.Features.MembershipTypes.Queries.GetMembershipTypeDataTable;
using GymGo.Application.Requests.DateTables;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymGo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipTypesController : ControllerBase
    {
        // GET: api/<MembershipTypesController>
        private readonly IMediator _mediator;

        public MembershipTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllMembershipType")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllMembershipTypeQuery());
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblemDetails();
        }

        // GET api/<MembershipTypesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetDetailMembershipTypeQuery(id));
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblemDetails();
        }

        // POST api/<MembershipTypesController>
        [HttpPost(Name = "AddMembershipType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Crear(
            [FromBody] CreateMembershipTypeCommand command
            )
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess
                ? StatusCode(StatusCodes.Status201Created, result.Value)
                : result.ToProblemDetails();
        }

        // PUT api/<MembershipTypesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MembershipTypesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteMembershipTypeCommand(id));
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblemDetails();
        }

        // POST api/<MembershipTypesController>
        [HttpPost("datatable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDataTable(
            [FromBody] DataTableRequest request
            )
        {
            var result = await _mediator.Send(new GetMembershipTypeDataTableQuery(request));
            return result.IsSuccess
                ? StatusCode(StatusCodes.Status200OK, result.Value)
                : result.ToProblemDetails();
        }
    }
}
