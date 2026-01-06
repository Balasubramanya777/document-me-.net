using Base.DataAccessLayer.DTO.Base;
using DocumentMe.API.Helper;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.Service.IService.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DocumentMe.DataAccessLayer.DTO.Public.SignInDTO;

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
        [HttpPost, Route("SaveUser")]
        public async Task<IActionResult> SaveUser(SignUpDTO userDto)
        {
            var response = await _userService.SaveUser(userDto);
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
