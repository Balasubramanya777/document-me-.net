using Base.DataAccessLayer.DTO.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocumentMe.API.Helper
{
    public static class ApiResponseExtensions
    {
        public static IActionResult ToActionResult<T>(this ApiResponse<T> response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => new OkObjectResult(response),
                HttpStatusCode.Created => new CreatedResult(string.Empty, response),
                HttpStatusCode.BadRequest => new BadRequestObjectResult(response),
                HttpStatusCode.NotFound => new NotFoundObjectResult(response),
                HttpStatusCode.Conflict => new ConflictObjectResult(response),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(response),
                HttpStatusCode.Forbidden => new ObjectResult(response) { StatusCode = 403 },
                HttpStatusCode.InternalServerError => new ObjectResult(response) { StatusCode = 500 },

                // default fallback
                _ => new ObjectResult(response) { StatusCode = (int)response.StatusCode }
            };
        }
    }
}
