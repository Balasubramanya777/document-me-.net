namespace DocumentMe.DataAccessLayer.DTO.Auth
{
    public class SignUpRequest
    {
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
    }
}
