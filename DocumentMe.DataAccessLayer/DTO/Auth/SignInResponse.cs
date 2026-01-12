using DocumentMe.DataAccessLayer.DTO.Public;

namespace DocumentMe.DataAccessLayer.DTO.Auth
{
    public class SignInResponse
    {
        public required string AccessToken { get; set; }
        public required UserDto User { get; set; }
    }
}
