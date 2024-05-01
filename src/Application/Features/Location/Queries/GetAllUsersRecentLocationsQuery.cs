using Application.Common;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Location.Queries
{
    public record GetAllUsersRecentLocationsQuery() : IRequest<Response<List<UserLocation>>>;

    internal sealed class GetAllUsersRecentLocationsQueryHandler : IRequestHandler<GetAllUsersRecentLocationsQuery, Response<List<UserLocation>>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;

        public GetAllUsersRecentLocationsQueryHandler(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }

        public async Task<Response<List<UserLocation>>> Handle(GetAllUsersRecentLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _localMemoryRepository.GetMostRecentLocationForAllUsers();
            if (locations == null || locations.Count == 0)
            {
                throw new LocationsNotFoundException("No Locations found.");
            }
            return Response<List<UserLocation>>.Success(locations);
        }
    }
}
