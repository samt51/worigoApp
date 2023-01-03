using Microsoft.AspNetCore.Mvc;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if (response.statusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.statusCode
                };
            return new ObjectResult(response)
            {
                StatusCode = response.statusCode
            };
        }
       
    }
}
