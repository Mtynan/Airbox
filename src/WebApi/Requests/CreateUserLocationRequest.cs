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
        /// <example>1</example>
        public int UserId { get; set; }
        /// <summary>
        /// The latitude of the location.
        /// </summary>
        /// <example>45</example>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude of the location.
        /// </summary>
        /// <example>-45</example>
        public double Longitude { get; set; }
    }
}
