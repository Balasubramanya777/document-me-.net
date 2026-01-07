using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Auth;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.DataAccessLayer.Entity.Public;

namespace DocumentMe.Service.IService.Public
{
    public interface IUserService
    {
        Task<User?> GetUserByUserName(string userName);
        Task<ApiResponse<bool>> CreateUser(SignUpRequest userDto);
        Task<ApiResponse<SignInResponse>> Authenticate(SignInRequest signInReq);
    }
}
