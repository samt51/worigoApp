using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.API.Filter
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(new ResponseDto<NoContentResult>().Fail(400, errors));
            }
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var errors = filterContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                filterContext.Result = new BadRequestObjectResult(new ResponseDto<NoContentResult>().Fail(400, errors));
            }
        }
    }
}