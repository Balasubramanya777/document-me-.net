using DocumentMe.API.Helper;
using DocumentMe.DataAccessLayer.DTO.Auth;
using DocumentMe.Service.IService.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost, Route("CreateUser")]
        public async Task<IActionResult> CreateUser(SignUpRequest userDto)
        {
            var response = await _userService.CreateUser(userDto);
            return response.ToActionResult();
        }

        //[AllowAnonymous]
        //[HttpPost("Authenticate")]
        //public async Task<IActionResult> Authenticate(SignInRequest signInReq)
        //{
        //    SignInResponse signInResponse = await _userService.Authenticate(signInReq);

        //    if (!string.IsNullOrEmpty(signInResponse.AccessToken) && signInResponse.User.Success)
        //    {
        //        Response.Cookies.Append("access_token", signInResponse.AccessToken, new CookieOptions
        //        {
        //            HttpOnly = true,
        //            Secure = false,
        //            SameSite = SameSiteMode.Lax,
        //            Expires = DateTime.UtcNow.AddSeconds(30000)
        //        });
        //    }
        //    return signInResponse.User.ToActionResult();
        //}

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(SignInRequest signInReq)
        {
            var response = await _userService.Authenticate(signInReq);
            return response.ToActionResult();
        }

    }
}
