using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;

namespace Worigo.Business.Abstrack
{
    public interface ICustomerService
    {
        ResponseDto<CustomerResponse> PostCustomerByCode(CustomerAddOrUpdate request, TokenKeys keys);
        ResponseDto<GetCustomerOfServicesResponse> GetCustomerOfServiceResponse(string code, TokenKeys keys);
        ResponseDto<GetCustomerOfServiceValueResponse> GetCustomerOfServiceValueResponse(int serviceId, string code, TokenKeys keys);
    }
}
