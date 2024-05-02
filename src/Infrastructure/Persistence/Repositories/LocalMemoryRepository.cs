using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LocalMemoryRepository : ILocalMemoryRepository
    {
        private List<UserLocation> _locations;
        private int _nextId = 1;

        public LocalMemoryRepository(List<UserLocation>? seedData = null)
        {
            if (seedData == null)
            {
                _locations = [];
                SeedData();
            } else
            {
                _locations = seedData;
                _nextId = seedData.Count + 1;
            }
        }
        private void SeedData()
        {
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 1, Latitude = 34.0522, Longitude = -118.2437, CreatedAt = DateTime.Now });
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 1, Latitude = 34.0522, Longitude = -118.2436, CreatedAt = DateTime.Now.AddMinutes(-10) });
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 2, Latitude = 40.7128, Longitude = -74.0060, CreatedAt = DateTime.Now });
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 2, Latitude = 40.7127, Longitude = -74.0059, CreatedAt = DateTime.Now.AddMinutes(-10) });
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 3, Latitude = 37.7749, Longitude = -122.4194, CreatedAt = DateTime.Now.AddMinutes(-10) });
            _locations.Add(new UserLocation { Id = _nextId++, UserId = 3, Latitude = 37.7749, Longitude = -122.4194, CreatedAt = DateTime.Now });
        }
        public Task<UserLocation> CreateLocation(UserLocation location)
        {
            location.Id = _nextId++;
            location.CreatedAt = DateTime.Now;
            _locations.Add(location);
            return Task.FromResult(location);
        }

        public Task<UserLocation?> GetMostRecentLocationByUserId(int userId)
        {
            var recentLocation = _locations.Where(x => x.UserId == userId).OrderByDescending(y => y.CreatedAt).FirstOrDefault();
            return Task.FromResult(recentLocation);
        }

        public Task<List<UserLocation>> GetAllUserLocationsByUserId(int userId)
        {
            var list = _locations.Where(x => x.UserId == userId).ToList();
            return Task.FromResult(list);
        }

        public Task<List<UserLocation>> GetMostRecentLocationForAllUsers()
        {
            var mostRecentLocations = _locations
                    .GroupBy(loc => loc.UserId)
                    .Select(group => group.OrderByDescending(loc => loc.CreatedAt).FirstOrDefault())
                    .OfType<UserLocation>()
                    .ToList();

            return Task.FromResult(mostRecentLocations);
        }
    }
}

