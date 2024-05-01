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
    public record GetMostRecentLocationQuery(int UserId) : IRequest<Response<UserLocation>>;
    internal sealed class GetMostRecentLocationQueryHandler : IRequestHandler<GetMostRecentLocationQuery, Response<UserLocation>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;
        public GetMostRecentLocationQueryHandler(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }

        public async Task<Response<UserLocation>> Handle(GetMostRecentLocationQuery request, CancellationToken cancellationToken)
        {
            var location = await _localMemoryRepository.GetMostRecentLocationByUserId(request.UserId);
            if(location == null)
            {
                throw new LocationsNotFoundException();
            }
            return Response<UserLocation>.Success(location);
        }
    }
}
