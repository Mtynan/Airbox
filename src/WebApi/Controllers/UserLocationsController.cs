using Application.Common;
using Application.Features.Location.Commands;
using Application.Features.Location.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers
{
    public class UserLocationsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserLocation(CreateUserLocationRequest req)
        {
            var result = await _mediator.Send(new CreateLocationCommand(req.Latitude, req.Longitude, req.UserId));
            return Ok(result);
        }

        [HttpGet("{userId:int}/recent")]
        public async Task<IActionResult> GetMostRecentUserLocation(int userId)
        {
            var result = await _mediator.Send(new GetMostRecentLocationQuery(userId));
            return Ok(result.Data);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetAllUserLocations(int userId)
        {
            var result = await _mediator.Send(new GetAllUserLocationsQuery(userId));
            return Ok(result.Data);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetAllUsersRecentLocations()
        {
            var result = await _mediator.Send(new GetAllUsersRecentLocationsQuery());
            return Ok(result.Data);
        }


    }
}
