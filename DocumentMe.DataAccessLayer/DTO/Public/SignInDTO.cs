using Base.DataAccessLayer.DTO.Base;

namespace DocumentMe.DataAccessLayer.DTO.Public
{
    public class SignInDTO
    {
        public class SignInRequest
        {
            public required string UserName { get; set; }
            public required string Password { get; set; }
        }

        //public class SignInResponse
        //{
        //    public required string AccessToken { get; set; }
        //    public required ApiResponse<UserDTO> User { get; set; }
        //}

        public class SignInResponse
        {
            public required string AccessToken { get; set; }
            public required UserDTO User { get; set; }
        }
    }
}
