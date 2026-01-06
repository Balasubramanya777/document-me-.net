using AutoMapper;
using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.DataAccessLayer.Entity.Public;
using DocumentMe.Repository.IRepository.Public;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Helper;
using DocumentMe.Utility.IUtility;
using DocumentMe.Utility.Resource;
using Microsoft.Extensions.Localization;
using System.Net;
using static DocumentMe.DataAccessLayer.DTO.Public.SignInDTO;

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

        public async Task<ApiResponse<bool>> SaveUser(SignUpDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.Password))
                return new ApiResponse<bool>(false, false, _messagesLocalizer["SignInInvalid"], HttpStatusCode.BadRequest);

            User? isExist = await _userRepository.GetUserByUserName(userDto.UserName);
            if (isExist != null)
                return new ApiResponse<bool>(false, false, _messagesLocalizer["AlreadyExistsWith", _labelsLocalizer["User"], _labelsLocalizer["UserName"], userDto.UserName], HttpStatusCode.Conflict);

            string hashedPassword = PasswordHasher.HashPassword(userDto.Password);

            User user = new()
            {
                UserName = userDto.UserName,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = hashedPassword,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.SaveUser(user);
            return new ApiResponse<bool>(true, true, _messagesLocalizer["SaveSuccess", _labelsLocalizer["User"]], HttpStatusCode.Created);
        }

        //public async Task<SignInResponse> Authenticate(SignInRequest signInReq)
        //{
        //    var (token, user) = await AuthenticateHelper(signInReq);
        //    if (string.IsNullOrEmpty(token) || user == null)
        //        return new SignInResponse
        //        {
        //            AccessToken = string.Empty,
        //            User = new ApiResponse<UserDTO>(null, false, _messagesLocalizer["SignInInvalid"], HttpStatusCode.BadRequest)
        //        };


        //    UserDTO userDto = _mapper.Map(user, new UserDTO());
        //    return new SignInResponse
        //    {
        //        AccessToken = token,
        //        User = new ApiResponse<UserDTO>(userDto, true, _messagesLocalizer["SignInSuccess"], HttpStatusCode.OK)
        //    };
        //}

        public async Task<ApiResponse<SignInResponse>> Authenticate(SignInRequest signInReq)
        {
            var (token, user) = await AuthenticateHelper(signInReq);
            if (string.IsNullOrEmpty(token) || user == null)
                return new ApiResponse<SignInResponse>(null, false, _messagesLocalizer["SignInInvalid"], HttpStatusCode.BadRequest);

            UserDTO userDto = _mapper.Map(user, new UserDTO());
            return new ApiResponse<SignInResponse>(new SignInResponse { AccessToken = token, User = userDto }, true, _messagesLocalizer["SignInSuccess"], HttpStatusCode.OK);
        }

        private async Task<(string?, User? user)> AuthenticateHelper(SignInRequest signInReq)
        {
            User? user = await _userRepository.GetUserByUserName(signInReq.UserName);
            if (user == null)
                return (null, null);

            if (!PasswordHasher.VerifyPassword(signInReq.Password, user.Password))
                return (null, user);

            return (_jwtToken.GenerateJWT(user.UserId.ToString(), user.Email), user);
        }

        //private string GenerateJWT(User user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(GetClaims(user)),
        //        Issuer = _configuration["JwtSettings:Issuer"],
        //        Audience = _configuration["JwtSettings:Audience"],
        //        Expires = DateTime.UtcNow.AddSeconds(30000),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //private static IEnumerable<Claim> GetClaims(User user)
        //{
        //    return
        //    [
        //        new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        //        new(JwtRegisteredClaimNames.Email, user.Email)
        //    ];
        //}
    }
}
