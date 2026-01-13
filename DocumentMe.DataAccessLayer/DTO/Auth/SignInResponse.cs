using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Public;

namespace DocumentMe.DataAccessLayer.DTO.Auth
{
    public class SignInResponse
    {
        public string? AccessToken { get; set; }
        public required ApiResponse<UserDto> User { get; set; }
    }
}
