using DocumentMe.API.Helper;
using DocumentMe.DataAccessLayer.DTO.Auth;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DocumentMe.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer<Messages> _messagesLocalizer;

        public UserController(IUserService userService, IStringLocalizer<Messages> localizer)
        {
            _userService = userService;
            _messagesLocalizer = localizer;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(SignInRequest signInReq)
        {
            SignInResponse signInResponse = await _userService.Authenticate(signInReq);
            if (signInResponse.User.Success && !string.IsNullOrEmpty(signInResponse.AccessToken))
            {
                Response.Cookies.Append("access_token", signInResponse.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, //true for prod
                    SameSite = SameSiteMode.Lax,//SameSiteMode.Strict for prod
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
            }
            return signInResponse.User.ToActionResult();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpRequest userDto)
        {
            var response = await _userService.CreateUser(userDto);
            return response.ToActionResult();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("access_token", string.Empty, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, //true for prod
                SameSite = SameSiteMode.Lax,//SameSiteMode.Strict for prod
                Expires = DateTimeOffset.UtcNow.AddHours(-1)
            });

            return Ok(_messagesLocalizer["AuthSignOutSuccess"]);
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok();
        }
    }
}
