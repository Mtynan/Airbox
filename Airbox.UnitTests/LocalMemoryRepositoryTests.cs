using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbox.UnitTests
{
    public class LocalMemoryRepositoryTests
    {
        [Fact]
        public async Task GetMostRecentLocationByUserId_WithValidData_ReturnsExpected()
        {
            //arrange
            var userId = 1;
            var data = new List<UserLocation>
            {
                new UserLocation { Id = 1, UserId = userId, Latitude = 55, Longitude = 55, CreatedAt = DateTime.Now },
                new UserLocation { Id = 2, UserId = userId, Latitude = 44, Longitude = 44, CreatedAt = DateTime.Now.AddMinutes(-10) },
            };

            var repo = new LocalMemoryRepository(data);

            //act

            var result = await repo.GetMostRecentLocationByUserId(userId);

            //asser
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetMostRecentLocationForAllUsers_WithValidData_ReturnsExpected()
        {
            //arrange
            var userId = 1;
            var data = new List<UserLocation>
            {
                new UserLocation { Id = 1, UserId = userId, Latitude = 55, Longitude = 55, CreatedAt = DateTime.Now },
                new UserLocation { Id = 2, UserId = userId, Latitude = 44, Longitude = 44, CreatedAt = DateTime.Now.AddMinutes(-10) },
                new UserLocation { Id = 3, UserId = userId + 1, Latitude = 33, Longitude = 33, CreatedAt = DateTime.Now },
                new UserLocation { Id = 4, UserId = userId + 1, Latitude = 11, Longitude = 11, CreatedAt = DateTime.Now.AddMinutes(-10) },
            };

            var repo = new LocalMemoryRepository(data);

            //act

            var results = await repo.GetMostRecentLocationForAllUsers();

            //asser
            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Equal(1, results.First().Id);
            Assert.Equal(3, results[1].Id);
        }

        [Fact]
        public async Task GetMostRecentLocationByUserId_WithInvalidUserId_ReturnsNull()
        {
            //arrange
            var userId = 1;
            var data = new List<UserLocation>
            {
                new UserLocation { Id = 1, UserId = 4, Latitude = 55, Longitude = 55, CreatedAt = DateTime.Now },
            };

            var repo = new LocalMemoryRepository(data);

            //act

            var results = await repo.GetMostRecentLocationByUserId(userId);

            //asser
            Assert.Null(results);
        }
    }
}
