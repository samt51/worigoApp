using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worigo.API.Model.UserViewModel;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.Order.Request;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Entity.Concrete;

namespace Worigo.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        TokenViewModel tkn = new TokenViewModel();
        public CustomerController(ICustomerService customerService, IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<ObjectResult> CustomerPostByCode(CustomerAddOrUpdate request)
        {
            var response = new ResponseDto<ObjectResult>();
            var customer = _customerService.PostCustomerByCode(request, null);
            if (customer.data != null)
            {
                return new ResponseDto<ObjectResult>().Success(200);
            }
            response.errors = customer.errors;
            return response;
        }
        [HttpGet("{code}")]
        public ResponseDto<GetCustomerOfServicesResponse> GetCustomerOfServiceResponse([FromHeader] string Authorization, string code)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _customerService.GetCustomerOfServiceResponse(code, keys);
        }
        [HttpGet("{serviceId}/{code}")]
        public ResponseDto<GetCustomerOfServiceValueResponse> GetCustomerOfServiceValueResponse([FromHeader] string Authorization, int serviceId, string code)
        {
            TokenKeys keys = AuthorizationCont.Authorization(Authorization);
            return _customerService.GetCustomerOfServiceValueResponse(serviceId, code, keys);
        }
    }
}
