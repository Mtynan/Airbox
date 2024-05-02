namespace WebApi.Requests
{
    /// <summary>
    /// The request to create a new user location.
    /// </summary>
    public sealed class CreateUserLocationRequest
    {
        /// <summary>
        /// The Id of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The latitude of the location.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude of the location.
        /// </summary>
        public double Longitude { get; set; }
    }
}
