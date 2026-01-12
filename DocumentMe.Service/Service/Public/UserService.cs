using AutoMapper;
using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Auth;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.DataAccessLayer.Entity.Public;
using DocumentMe.Repository.IRepository.Public;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Helper;
using DocumentMe.Utility.IUtility;
using DocumentMe.Utility.Resource;
using Microsoft.Extensions.Localization;
using System.Net;

namespace DocumentMe.Service.Service.Public
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<Messages> _messagesLocalizer;
        private readonly IStringLocalizer<Labels> _labelsLocalizer;
        private readonly IMapper _mapper;
        private readonly IJwtToken _jwtToken;

        public UserService(IUserRepository userRepository, IStringLocalizer<Messages> messagesLocalizer, IStringLocalizer<Labels> labelsLocalizer,
            IMapper mapper, IJwtToken jwtToken)
        {
            _userRepository = userRepository;
            _messagesLocalizer = messagesLocalizer;
            _labelsLocalizer = labelsLocalizer;
            _mapper = mapper;
            _jwtToken = jwtToken;
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _userRepository.GetUserByUserName(userName);
        }

        public async Task<ApiResponse<bool>> CreateUser(SignUpRequest userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.FirstName) || string.IsNullOrWhiteSpace(userDto.LastName)
                 || string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
                return new ApiResponse<bool>(false, false, _messagesLocalizer["AuthSignInInvalid"], HttpStatusCode.BadRequest);

            User? isExist = await _userRepository.GetUserByUserName(userDto.UserName);
            if (isExist != null)
                return new ApiResponse<bool>(false, false, _messagesLocalizer["ErrorAlreadyExistsWith", _labelsLocalizer["User"], _labelsLocalizer["UserName"], userDto.UserName], HttpStatusCode.Conflict);

            string hashedPassword = PasswordHasher.HashPassword(userDto.Password);

            User user = new()
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = hashedPassword,
                IsActive = true,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            await _userRepository.CreateUser(user);
            return new ApiResponse<bool>(true, true, _messagesLocalizer["ResponseSaveSuccess", _labelsLocalizer["User"]], HttpStatusCode.Created);
        }

        //public async Task<SignInResponse> Authenticate(SignInRequest signInReq)
        //{
        //    var (token, user) = await AuthenticateHelper(signInReq);
        //    if (string.IsNullOrEmpty(token) || user == null)
        //        return new SignInResponse
        //        {
        //            AccessToken = string.Empty,
        //            User = new ApiResponse<UserDto>(null, false, _messagesLocalizer["AuthSignInInvalid"], HttpStatusCode.BadRequest)
        //        };


        //    UserDto userDto = _mapper.Map(user, new UserDto());
        //    return new SignInResponse
        //    {
        //        AccessToken = token,
        //        User = new ApiResponse<UserDto>(userDto, true, _messagesLocalizer["AuthSignInSuccess"], HttpStatusCode.OK)
        //    };
        //}

        public async Task<ApiResponse<SignInResponse>> Authenticate(SignInRequest signInReq)
        {
            User? user = await _userRepository.GetUserByUserName(signInReq.UserName);
            if (user == null)
                return new ApiResponse<SignInResponse>(null, false, _messagesLocalizer["AuthSignInInvalid"], HttpStatusCode.BadRequest);

            string? token = AuthenticateHelper(signInReq, user);
            if (string.IsNullOrEmpty(token))
                return new ApiResponse<SignInResponse>(null, false, _messagesLocalizer["AuthSignInInvalid"], HttpStatusCode.BadRequest);

            UserDto userDto = _mapper.Map(user, new UserDto());
            return new ApiResponse<SignInResponse>(new SignInResponse { AccessToken = token, User = userDto }, true, _messagesLocalizer["AuthSignInSuccess"], HttpStatusCode.OK);
        }

        private string? AuthenticateHelper(SignInRequest signInReq, User user)
        {
            if (!PasswordHasher.VerifyPassword(signInReq.Password, user.Password))
                return null;

            return _jwtToken.GenerateJWT(user.UserId.ToString(), user.Email);
        }
    }
}
