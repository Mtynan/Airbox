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
    public record GetAllUserLocationsQuery(int UserId) : IRequest<Response<List<UserLocation>>>;

    internal sealed class GetAllUserLocationsQueryHandler : IRequestHandler<GetAllUserLocationsQuery, Response<List<UserLocation>>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;

        public GetAllUserLocationsQueryHandler(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }

        public async Task<Response<List<UserLocation>>> Handle(GetAllUserLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _localMemoryRepository.GetAllUserLocationsByUserId(request.UserId);
            if (locations == null || locations.Count == 0)
            {
                throw new LocationsNotFoundException();
            }
            return Response<List<UserLocation>>.Success(locations);
        }
    }
}
