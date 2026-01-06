using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.DataAccessLayer.Entity.Public;
using static DocumentMe.DataAccessLayer.DTO.Public.SignInDTO;

namespace DocumentMe.Service.IService.Public
{
    public interface IUserService
    {
        Task<User?> GetUserByUserName(string userName);
        Task<ApiResponse<bool>> SaveUser(SignUpDTO userDto);
        Task<ApiResponse<SignInResponse>> Authenticate(SignInRequest signInReq);
    }
}
