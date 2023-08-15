using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.Customer.Request;
using Worigo.Core.Dtos.Customer.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;

namespace Worigo.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IManagementOfHotelsDal _managementOfHotelsDal;
        private readonly IVerificationCodeDal _verificationCodeDal;
        public CustomerManager(IVerificationCodeDal verificationCodeDal, ICustomerDal customerDal, IManagementOfHotelsDal managementOfHotelsDal)
        {
            _customerDal = customerDal;
            _managementOfHotelsDal = managementOfHotelsDal;
            _verificationCodeDal = verificationCodeDal;
        }

        public ResponseDto<GetCustomerOfServicesResponse> GetCustomerOfServiceResponse(string code, TokenKeys keys)
        {
            var vr = _verificationCodeDal.GetAll(x => x.id == keys.userId);
            //_managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, vr[0].hotelid, keys.companyid);
            return _customerDal.GetCustomerOfServiceResponse(code);
        }

        public ResponseDto<GetCustomerOfServiceValueResponse> GetCustomerOfServiceValueResponse(int serviceId, string code, TokenKeys keys)
        {
            var vr = _verificationCodeDal.GetAll(x => x.id == keys.userId);
            //_managementOfHotelsDal.AuthorizeControll(keys.role, keys.userId, vr[0].hotelid, keys.companyid);
            return _customerDal.GetCustomerOfServiceValueResponse(serviceId, code);
        }

        public ResponseDto<CustomerResponse> PostCustomerByCode(CustomerAddOrUpdate request, TokenKeys keys)
        {
            return _customerDal.PostCustomerByCode(request);
        }
    }
}
