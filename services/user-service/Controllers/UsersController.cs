using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Reflection.Metadata.Ecma335;
using user_service.Controllers;
using user_service.Handlers;
using user_service_core;
using static user_service.Handlers.AddUserCommandHandler;
using static user_service.Handlers.AddUsersInBulkCommandHandler;
using static user_service.Handlers.DeleteUserCommandHandler;
using static user_service.Handlers.FindUserByIdQueryHandler;
using static user_service.Handlers.GetUsersRequestHandler;
using static user_service.Handlers.UpdateUserCommandHandler;

namespace users_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        //private IEnumerable<string> Fleets = new[]
        //{
        //"Fleet1", "Fleet2", "Fleet3", "Fleet4", "Fleet5"
        //};


        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            //HttpClientHandler handler= new HttpClientHandler();
            //handler.Credentials
        }
        [HttpGet()]
        [Authorize(Policy ="can-list-users")]
        public async Task<ActionResult<GetUsersResponse>> Get()
        {

            var response = await _mediator.Send(new GetUsersQuery());
            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return NotFound(response);
            }
            return Ok(response);

            
        }
        [HttpGet()]
        [Route("{id}")]
        //[Authorize(Policy ="can-read-fleet")]
        public async Task<ActionResult<FindUserByIdQueryResponse>> GetUser([FromRoute] string id)
        {

            var response = await _mediator.Send(new FindUserByIdQuery(id));

            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return NotFound(response);
            }
            return Ok(response);
        }



        [HttpPost()]
        //[Authorize("can-add-user")]
        public async Task<ActionResult<AddUserResponse>> Add([FromBody] AddUserCommand userToAdd)
        {
            //TODO: Add fluent validation for the command as behavior may be!
            var response = await _mediator.Send(userToAdd);
            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return StatusCode(500, response);
            }
            return CreatedAtAction(nameof(GetUser), new { id = response.CreatedUser.Id }, response);
        }

        [HttpPost()]
        [Route("batch")]
        // [Authorize("can-add-fleet")]
        public async Task<ActionResult<AddUsersInBulkResponse>> Add()
        {
            //TODO: Add fluent validation for the command as behavior may be!
            var response = await _mediator.Send(new AddUsersInBulkCommand());
            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return StatusCode(500, response);
            }
            return Accepted(response);
        }

        [HttpPut("{id}")]
        // [Authorize("can-add-fleet")]
        public async Task<ActionResult<UpdateUserResponse>> Update([FromRoute]string id,[FromBody] UserUpdateDTO userToUpdate)
        {
            //TODO: Add fluent validation for the command as behavior may be!
            var response = await _mediator.Send(new UpdateUserCommand(id,userToUpdate));
            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return StatusCode((int)response?.Errors?.FirstOrDefault()?.Status, response);
            }
            return Ok(response);
        }

        [HttpDelete()]
        [Route("{id}")]
        // [Authorize("can-delete-user")]
        public async Task<ActionResult<DeleteUserResponse>> Delete([FromRoute] string id)
        {
            //TODO: Add fluent validation for the command as behavior may be!
            var response = await _mediator.Send(new DeleteUserCommand(id));
            if (!(bool)response?.Errors.IsNullOrEmpty())
            {
                return StatusCode((int)response?.Errors?.FirstOrDefault()?.Status, response);
            }
            return NoContent();
        }
    }
}