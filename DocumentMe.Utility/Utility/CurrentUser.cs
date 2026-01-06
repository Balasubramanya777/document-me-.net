using DocumentMe.Utility.IUtility;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DocumentMe.Utility.Utility
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    }
}
