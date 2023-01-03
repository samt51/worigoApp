namespace Worigo.API.Filter
{
    //public class NotFoundFilter<T> : IAsyncActionFilter
    //{
    //    private IRepositoryDesignPattern<T> _hotelService;
    //    public NotFoundFilter(IRepositoryDesignPattern<T> hotelService)
    //    {
    //        _hotelService = hotelService;
    //    }
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        var idvalue = context.ActionArguments.Values.FirstOrDefault();
    //        if (idvalue == null)
    //        {
    //            await next.Invoke();
    //        }
    //        var id = (int)idvalue;
    //        var result =  _hotelService.GetById(id);
    //        if (result!=null)
    //        {
    //            await next.Invoke();
    //            return;
    //        }
    //        context.Result = new NotFoundObjectResult(ResponseDto<Hotel>.Success(400, $"{typeof(Hotel).Name}({id}",0));
    //    }
    //}
}
