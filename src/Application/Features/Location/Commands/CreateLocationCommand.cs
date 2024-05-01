using MediatR;
using Domain.Entities;
using Domain.Interfaces;
using Application.Common;


namespace Application.Features.Location.Commands
{
    public record CreateLocationCommand(double Latitude, double Longitude, int UserId) : IRequest<Response<UserLocation>>;

    internal sealed class CreateLocationHandler : IRequestHandler<CreateLocationCommand, Response<UserLocation>>
    {
        private readonly ILocalMemoryRepository _localMemoryRepository;

        public CreateLocationHandler(ILocalMemoryRepository localMemoryRepository)
        {
            _localMemoryRepository = localMemoryRepository;
        }

        public async Task<Response<UserLocation>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
           
            var location = new UserLocation
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                UserId = request.UserId
            };

            var createdLocation = await _localMemoryRepository.CreateLocation(location);
            return Response<UserLocation>.Success(createdLocation);
        }
    }
}
