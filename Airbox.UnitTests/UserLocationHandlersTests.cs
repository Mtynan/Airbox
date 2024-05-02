using Application.Features.Location.Commands;
using Application.Features.Location.Queries;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Airbox.UnitTests
{
    public class UserLocationHandlersTests
    {
        private readonly Mock<ILocalMemoryRepository> _mockRepo;

        public UserLocationHandlersTests()
        {
            _mockRepo = new Mock<ILocalMemoryRepository>();
        }

        [Fact]
        public async Task GetMostRecentLocationQuery_InvalidUser_ThrowsLocationsNotFoundException()
        {
            //arrange
            var userId = 99;
            var handler = new GetMostRecentLocationQueryHandler(_mockRepo.Object);
            var query = new GetMostRecentLocationQuery(userId);
            _mockRepo.Setup(r => r.GetMostRecentLocationByUserId(userId)).ReturnsAsync((UserLocation?)null);

            //assert
            var exception = await Assert.ThrowsAsync<LocationsNotFoundException>(() => handler.Handle(query, CancellationToken.None));
            Assert.Equal("No locations found for the user.", exception.Message);
        }

        [Fact]
        public async Task CreateLocationCommand_ValidRequest_ReturnsSuccess()
        {
            // arrange
            var handler = new CreateLocationHandler(_mockRepo.Object);
            var command = new CreateLocationCommand(22, 22, 1);

            var expectedLocation = new UserLocation
            {
                Latitude = 22,
                Longitude = 22,
                UserId = 1,
                Id = 10  
            };

            _mockRepo.Setup(x => x.CreateLocation(It.IsAny<UserLocation>()))
                          .ReturnsAsync(expectedLocation);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            _mockRepo.Verify(x => x.CreateLocation(It.IsAny<UserLocation>()), Times.Once);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedLocation.Id, result.Data.Id);
          
        }
    }
}
