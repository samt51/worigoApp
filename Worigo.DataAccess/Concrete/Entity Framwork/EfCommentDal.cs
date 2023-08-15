using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Comment.Request;
using Worigo.Core.Dtos.Comment.Response;
using Worigo.Core.Dtos.Reports.HotelGeneralPuan;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Exceptions;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfCommentDal : EfRepositoryDal<Comment, DataContext>, ICommentDal
    {
        private readonly IMapper _mapper;
        public EfCommentDal(IMapper mapper)
        {
            _mapper = mapper;
        }
        public CommentResponse GetByIdJoin(int id)
        {
            using (var db = new DataContext())
            {
                var joinsorgu = from d1 in db.Comment.Where(x => x.Id == id)
                                join d2 in db.Order on d1.OrderId equals d2.id
                                join d3 in db.Employees on d2.EmployeeId equals d3.id
                                join d4 in db.employeesType on d3.employeestypeid equals d4.id
                                join d5 in db.Hotel on d2.hotelid equals d5.id
                                join d6 in db.ServicesValues on d2.serviceValueId equals d6.id
                                select new CommentResponse
                                {
                                    Id = d1.Id,
                                    EmployeeNameAndSurname = d3.Name + " " + d3.Surname,
                                    Commentary = d1.Commentary,
                                    EmployeePoint = d1.EmployeePoint,
                                    contentsPoint = d1.contentsPoint,
                                    speedPoint = d1.speedPoint,
                                    GeneralPoint = (d1.EmployeePoint + d1.contentsPoint + d1.speedPoint) / 3,
                                    employeesid = (int)d2.EmployeeId,
                                    EmployeePositionName = d4.TypeName,
                                    Service = d6.value,
                                    CreateDate = d2.CreatedDate,
                                    Hotel = d5.HotelName,
                                    hotelid = d5.id
                                };
                var data = joinsorgu.FirstOrDefault();
                return data;
            }
        }
        public List<CommentResponse> GetCommentByHotelid(int hotelid)
        {
            using (var db = new DataContext())
            {
                var joinList = from d1 in db.Comment.Where(x => x.hotelid == hotelid)
                               join d2 in db.Order on d1.OrderId equals d2.id
                               join d3 in db.Employees on d2.EmployeeId equals d3.id
                               join d4 in db.employeesType on d3.employeestypeid equals d4.id
                               join d5 in db.Hotel on d2.hotelid equals d5.id
                               join d6 in db.ServicesValues on d2.serviceValueId equals d6.id
                               select new CommentResponse
                               {
                                   Id = d1.Id,
                                   EmployeeNameAndSurname = d3.Name + " " + d3.Surname,
                                   Commentary = d1.Commentary,
                                   EmployeePoint = d1.EmployeePoint,
                                   contentsPoint = d1.contentsPoint,
                                   speedPoint = d1.speedPoint,
                                   GeneralPoint = (d1.EmployeePoint + d1.contentsPoint + d1.speedPoint) / 3,
                                   employeesid = (int)d2.EmployeeId,
                                   EmployeePositionName = d4.TypeName,
                                   Service = d6.value,
                                   CreateDate = d2.CreatedDate,
                                   Hotel = d5.HotelName,
                                   hotelid = d5.id
                               };
                return joinList.OrderByDescending(x => x.CreateDate).ToList();
            }
        }

        public ResponseDto<List<CommentResponse>> GetEmployeesOfCommentByHotelidAndEmployeesid(int hotelid, int employeeid)
        {
            return new ResponseDto<List<CommentResponse>>().Success(200);
        }

        public ResponseDto<List<GetOrderCommentResponse>> GetOrderCommentByVertificationId(int vertificationId)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.Order
                            join d2 in db.Comment on d1.id equals d2.OrderId
                            join d3 in db.Employees on d1.EmployeeId equals d3.id
                            join d4 in db.employeesType on d3.employeestypeid equals d4.id
                            join d5 in db.Hotel on d1.hotelid equals d5.id
                            join d6 in db.ServicesValues on d1.serviceValueId equals d6.id
                            join d7 in db.vertificationCodes on d1.VertificationId equals d7.id
                            join d8 in db.Room on d7.roomid equals d8.id
                            select new GetOrderCommentResponse
                            {
                                OrderId = d1.id,
                                speedPoint = d2.speedPoint,
                                contentsPoint = d2.contentsPoint,
                                EmployeePoint = d2.EmployeePoint,
                                Description = d1.Description,
                                ServiceId = d1.serviceValueId,
                                ServiceValue = d6.value,
                                Commentary = d2.Commentary,
                                CompletedDate = d1.CompletedDate,
                                CustomerId = d1.customerId,
                                hotelid = d1.hotelid,
                                GeneralPoint = (d2.speedPoint + d2.contentsPoint + d2.EmployeePoint) / 3,
                                OrderDate = d1.orderDate,
                                HotelName = d5.HotelName,
                                Id = d1.id,
                                Quantity = d1.Quantity,
                                RoomId = d7.roomid,
                                RoomNo = d8.RoomNo.ToString()
                            };
                return new ResponseDto<List<GetOrderCommentResponse>>().Success(query.ToList(), 200);
            }

        }

        public HotelGeneralPointResponse HotelGeneralPointByHotelId(int hotelid)
        {
            using (var db = new DataContext())
            {
                var GeneralHotelPointList = db.Comment.Where(x => x.hotelid == hotelid && x.isDeleted == false).ToList();
                var HotelName = db.Hotel.Where(x => x.id == hotelid && x.isDeleted == false).FirstOrDefault();
                if (HotelName == null)
                    throw new ClientSideException("Hotel Not Found");
                var EmployeePoint = 0;
                var SpeedPoint = 0;
                var ServicePoint = 0;
                EmployeePoint = GeneralHotelPointList.Sum(x => x.EmployeePoint);
                SpeedPoint = GeneralHotelPointList.Sum(x => x.speedPoint);
                ServicePoint = GeneralHotelPointList.Sum(x => x.contentsPoint);
                var employe = Convert.ToDecimal(Convert.ToDecimal(EmployeePoint / Convert.ToDecimal((GeneralHotelPointList.Count() * 3))).ToString("0.#"));
                var speed = Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(SpeedPoint) / GeneralHotelPointList.Select(x => x.speedPoint).Count()).ToString("0.#"));
                var service = Convert.ToDecimal((Convert.ToDecimal(ServicePoint) / GeneralHotelPointList.Count()).ToString("0.#"));
                var GeneralSum = Convert.ToDecimal((employe + speed + service) / GeneralHotelPointList.Count);
                var entity = new HotelGeneralPointResponse
                {
                    HotelName = HotelName.HotelName,
                    EmployeePoint = employe,
                    SpeedPoint = speed,
                    ServicePoint = service,
                    HotelAveragePoint = GeneralSum,
                    CommentCount = GeneralHotelPointList.Count
                };
                return entity;
            }
        }

        public ResponseDto<CommentResponse> PostCommentByOrderId(CommentAddOrUpdateRequest request)
        {
            var mapper = _mapper.Map<Comment>(request);
            var save = Create(mapper);
            return new ResponseDto<CommentResponse>().Success(200);
        }


    }
}
