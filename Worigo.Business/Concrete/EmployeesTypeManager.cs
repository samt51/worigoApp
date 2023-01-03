using AutoMapper;
using System.Collections.Generic;
using Worigo.Business.Abstrack;
using Worigo.Core.Dtos.EmployeeType.Request;
using Worigo.Core.Dtos.EmployeeType.Response;
using Worigo.Core.Dtos.JoinClass.AuthorizationClassView;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.DataAccess.Abstrack;
using Worigo.Entity.Concrete;

namespace Worigo.Business.Concrete
{
    public class EmployeesTypeManager : IEmployeesTypeService
    {
        private readonly IEmployeesTypeDal _employeesTypeDal;
        private readonly IMapper _mapper;
        private readonly IDepartmanDal _departmanDal;
        public EmployeesTypeManager(IEmployeesTypeDal employeesTypeDal, IMapper mapper, IDepartmanDal departmanDal)
        {
            _employeesTypeDal = employeesTypeDal;
            _mapper = mapper;
            _departmanDal = departmanDal;
        }



        public ResponseDto<EmployeeTypeResponse> Create(EmployeeTypeAddOrUpdateRequest entity, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                _departmanDal.GetById(entity.departmanid);
                var data = _employeesTypeDal.Create(_mapper.Map<EmployeesType>(entity));
                return new ResponseDto<EmployeeTypeResponse>().Success(_mapper.Map<EmployeeTypeResponse>(data), 200);
            }
            return new ResponseDto<EmployeeTypeResponse>().Authorization();
        }
 
        public ResponseDto<EmployeeTypeResponse> GetById(int id, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                var data = _employeesTypeDal.GetById(id);
                return new ResponseDto<EmployeeTypeResponse>().Success(_mapper.Map<EmployeeTypeResponse>(data), 200);
            }
            return new ResponseDto<EmployeeTypeResponse>().Authorization();
        }

    

        public ResponseDto<List<EmployeeTypeResponse>> GetEmployeesTypeByDepartmanid(int departmanid, TokenKeys keys)
        {
            return new ResponseDto<List<EmployeeTypeResponse>>().Success(_employeesTypeDal.GetEmployeesTypeByDepartmanid(departmanid), 200);
        }

        public EmployeesType Update(EmployeesType entity)
        {
            return _employeesTypeDal.Update(entity);
        }

        public ResponseDto<EmployeeTypeResponse> Update(EmployeeTypeAddOrUpdateRequest entity, TokenKeys keys)
        {
            if (keys.role >= 1 && keys.role <= 3)
            {
                _departmanDal.GetById(entity.departmanid);
                var data = _employeesTypeDal.Update(_mapper.Map<EmployeesType>(entity));
                return new ResponseDto<EmployeeTypeResponse>().Success(_mapper.Map<EmployeeTypeResponse>(data), 200);
            }
            return new ResponseDto<EmployeeTypeResponse>().Authorization();
        }
    }
}
