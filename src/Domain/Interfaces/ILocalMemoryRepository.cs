using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILocalMemoryRepository
    {
        Task<UserLocation> CreateLocation(UserLocation location);
        Task<UserLocation?> GetMostRecentLocationByUserId(int userId);
        Task<List<UserLocation>> GetAllUserLocationsByUserId(int userId);
        Task<List<UserLocation>> GetMostRecentLocationForAllUsers();
    }
}
