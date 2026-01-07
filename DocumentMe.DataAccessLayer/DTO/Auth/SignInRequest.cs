namespace DocumentMe.DataAccessLayer.DTO.Auth
{
    public class SignInRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
