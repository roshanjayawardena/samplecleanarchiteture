using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Application.Features.Property.Commands.CreateProperty;
using PropertyManagement.Application.Features.Property.Commands.DeleteProperty;
using PropertyManagement.Application.Features.Property.Commands.UpdateProperty;
using PropertyManagement.Application.Features.Property.Queries.GetPropertyList;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PropertyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userId}", Name = "GetProperties")]
        [ProducesResponseType(typeof(IEnumerable<PropertyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<PropertyVm>>> GetPropertiesById(string userId)
        {           
            var properties = await _mediator.Send(new GetPropertyListRequest() { OwnerId = userId });
            return Ok(properties);
        }

        // testing purpose
        [HttpPost(Name = "CreateProperty")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateProperty([FromBody] CreatePropertyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateProperty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateProperty([FromBody] UpdatePropertyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProperty")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeletePropertyCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
