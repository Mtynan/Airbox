using Application.Common;
using Application.Features.Location.Commands;
using Application.Features.Location.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers
{
    /// <summary>
    /// Manages User Locations.
    /// </summary>
    public class UserLocationsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public UserLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Creates a new User Location
        /// </summary>
        /// <param name="req">The request needed to create a new User Location.</param>
        /// <response code="200">Returns if the User Location is successfully created.</response>
        /// <response code="400">Returns if the request contains validation errors.</response>
        [HttpPost]
        public async Task<IActionResult> CreateUserLocation(CreateUserLocationRequest req)
        {
            var result = await _mediator.Send(new CreateLocationCommand(req.Latitude, req.Longitude, req.UserId));
            return Ok(result); 
        }
        /// <summary>
        /// Gets the most recent user location by User Id.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <response code="200">Returns if the retrival of the users location is successful.</response>
        /// <response code="400">Returns if there's a validation error with the request.</response>
        /// <response code="404">Returns if there is no data to retrive.</response>
        [HttpGet("{userId:int}/recent")]
        public async Task<IActionResult> GetMostRecentUserLocation(int userId)
        {
            var result = await _mediator.Send(new GetMostRecentLocationQuery(userId));
            return Ok(result.Data);
        }
        /// <summary>
        /// Gets All of the users known locations by User Id.
        /// </summary>
        /// <param name="userId">The Id of the user.</param>
        /// <response code="200">Returns if the retrival of the users locations is successful.</response>
        /// <response code="400">Returns if there's a validation error with the request.</response>
        /// <response code="404">Returns if there is no data to retrive.</response>
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetAllUserLocations(int userId)
        {
            var result = await _mediator.Send(new GetAllUserLocationsQuery(userId));
            return Ok(result.Data);
        }
        /// <summary>
        /// Gets all users most recent locations.
        /// </summary>
        /// <response code="200">Returns if the retrival of the user locations is successful.</response>
        /// <response code="404">Returns there is no data to retrive.</response>
        [HttpGet("recent")]
        public async Task<IActionResult> GetAllUsersRecentLocations()
        {
            var result = await _mediator.Send(new GetAllUsersRecentLocationsQuery());
            return Ok(result.Data);
        }


    }
}
