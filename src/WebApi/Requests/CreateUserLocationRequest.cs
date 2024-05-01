namespace WebApi.Requests
{
    public sealed class CreateUserLocationRequest
    {
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
