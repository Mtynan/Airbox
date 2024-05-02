using Application.Common;
using Application.Features.Location.Queries;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Airbox.UnitTests
{
    public class UserLocationControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly UserLocationsController _userLocationsController;

        public UserLocationControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _userLocationsController = new UserLocationsController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetMostRecentUserLocation_ValidUser_ReturnsOK()
        {
            //arrange
            int userId = 1;
            var userLocation = new UserLocation { Id = 1, CreatedAt = DateTime.Now, Latitude = 55, Longitude = 55 };
            var userLocationResponse = new Response<UserLocation>
            {
                IsSuccess = true,
                Data = userLocation,
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetMostRecentLocationQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userLocationResponse));

            //act
            var result = await _userLocationsController.GetMostRecentUserLocation(userId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(userLocationResponse.Data, okResult.Value);
        }

        [Fact]
        public async Task GetAllUserLocations_ValidUser_ReturnsOK()
        {
            //arrange
            int userId = 1;
            var userLocations = new List<UserLocation> { new UserLocation { Id = 1, CreatedAt = DateTime.Now, Latitude = 55, Longitude = 55 } };
            var userLocationResponse = new Response<List<UserLocation>>
            {
                IsSuccess = true,
                Data = userLocations,
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetAllUserLocationsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userLocationResponse));

            //act
            var result = await _userLocationsController.GetAllUserLocations(userId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(userLocationResponse.Data, okResult.Value);
        }

    }
}
