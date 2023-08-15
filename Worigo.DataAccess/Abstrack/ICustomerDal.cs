using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack.Repository;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Abstrack
{
    public interface ICustomerDal : IRepositoryDesignPattern<Customer>
    {
        ResponseDto<CustomerResponse> PostCustomerByCode(CustomerAddOrUpdate request);
        ResponseDto<GetCustomerOfServicesResponse> GetCustomerOfServiceResponse(string code);
        ResponseDto<GetCustomerOfServiceValueResponse> GetCustomerOfServiceValueResponse(int serviceId,string code);
    }
}
