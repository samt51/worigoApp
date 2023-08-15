using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Worigo.Core.Dtos.Reports.Response;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Dtos.VerificationCodeDto.Dto;
using Worigo.Core.Dtos.VerificationCodeDto.Request;
using Worigo.Core.Dtos.VerificationCodeDto.Response;
using Worigo.Core.Exceptions;
using Worigo.Core.Extension;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.CodeFirst;
using Worigo.Entity.Concrete;

namespace Worigo.DataAccess.Concrete.Entity_Framwork
{
    public class EfVerificationCodeDal : EfRepositoryDal<VerificationCodes, DataContext>, IVerificationCodeDal
    {
        private readonly IMapper _mapper;
        public EfVerificationCodeDal(IMapper mapper)
        {
            _mapper = mapper;

        }

        public ResponseDto<VerificationCodeForAccessDto> CodeForAccess(ForAccessRequestModel requestModel)
        {
            using (var db = new DataContext())
            {
                var data = db.vertificationCodes.Where(x => x.Phone == requestModel.Phone && x.Code == requestModel.Code && x.IsFull == false && x.isDeleted == false).ToList();
                if (data.Count == 0)
                {
                    return new ResponseDto<VerificationCodeForAccessDto>().Fail(2, "Not Found");
                }

                var query = from d1 in data
                            join d2 in db.Hotel on d1.hotelid equals d2.id
                            join d3 in db.Room on d1.roomid equals d3.id

                            select new VerificationCodeForAccessDto
                            {
                                id = d1.id,
                                roomid = d1.roomid,
                                hotelid = d2.id,
                                StartDate = d1.StartDate,
                                FinishDate = d1.FinishDate,
                                Code = d1.Code,
                                Phone = d1.Phone,
                                Email = "",
                                FullName = "",
                                HotelName = d2.HotelName,
                                IsFull = d1.IsFull,
                                Price = d1.Price,
                                RoomNo = d3.RoomNo.ToString(),
                                Token = null,
                                CustomerId = d1.CustomerId,
                                CompanyId = d2.Companyid
                            };

                var result = query.FirstOrDefault();

                if (result.CustomerId is not null)
                {
                    var customer = db.Customers.Where(x => x.Id == (int)result.CustomerId).FirstOrDefault();
                    if (customer == null)
                        throw new ClientSideException($"{typeof(Customer).Name}  Not Found");
                    query.FirstOrDefault().FullName = string.Concat(customer.Name, " ", customer.Surname);
                    query.FirstOrDefault().Email = customer.Email;
                    query.FirstOrDefault().Phone = customer.Phone;
                }

                return new ResponseDto<VerificationCodeForAccessDto>().Success(query.FirstOrDefault(), 200);
            }
        }

        public ResponseDto<VerificationCodeResponse> CodeForAccess(string code)
        {
            using (var db = new DataContext())
            {
                var data = db.vertificationCodes.Where(x => x.Code == code && x.IsFull == false && x.isDeleted == false).FirstOrDefault();
                if (data == null)
                {
                    return new ResponseDto<VerificationCodeResponse>().Fail(200, "Not Found");
                }
                var query = from d1 in db.vertificationCodes
                            join d2 in db.Hotel on d1.hotelid equals d2.id
                            join d3 in db.Room on d1.roomid equals d3.id
                            where d1.id == data.id && data.isDeleted == false
                            select new VerificationCodeResponse
                            {
                                id = d1.id,
                                hotelid = d2.id,
                                StartDate = d1.StartDate,
                                FinishDate = d1.FinishDate,
                                Code = d1.Code,
                                CustomerId = d1.CustomerId,
                                HotelName = d2.HotelName,
                                roomid = d3.id,
                                RoomNo = d3.RoomNo.ToString(),
                                IsFull = d1.IsFull,
                                Price = d1.Price,
                            };

                var result = query.FirstOrDefault();

                if (result.CustomerId is not null)
                {
                    var customer = db.Customers.Where(x => x.Id == (int)result.CustomerId).FirstOrDefault();
                    if (customer == null)
                        throw new ClientSideException($"{typeof(Customer).Name}  Not Found");
                    query.FirstOrDefault().FullName = string.Concat(customer.Name, " ", customer.Surname);
                    query.FirstOrDefault().Email = customer.Email;
                    query.FirstOrDefault().Phone = customer.Phone;
                }
                return new ResponseDto<VerificationCodeResponse>().Success(query.FirstOrDefault(), 200);

            }
        }

        public List<CheckinTimeResponse> GetCheckinTimeByDate(int hotelid, DateTime startDate, DateTime endDate)
        {
            using (var db = new DataContext())
            {
                var query = from d1 in db.Hotel
                            join d2 in db.Room on d1.id equals d2.hotelid
                            join d3 in db.vertificationCodes on d2.RoomNo equals d3.roomid
                            where d2.isDeleted == false && d3.IsFull == false && d3.StartDate >= startDate && d3.FinishDate <= endDate
                            select new CheckinTimeResponse
                            {
                                startDate = startDate,
                                endDate = endDate,
                                hotelId = hotelid,
                                hotelName = d1.HotelName,
                                room = d2.RoomNo.ToString(),
                                roomId = d2.id
                            };
                return query.ToList();
            }
        }

        public VerificationCodes GetCodesByRoomid(int roomnumber)
        {
            throw new System.NotImplementedException();
        }

        public RoomCountResponse GetRoomCountByDate(int hotelid, DateTime date)
        {
            using (var db = new DataContext())
            {
                var allroomrate = db.Room.Where(x => x.hotelid == hotelid).ToList().Count;
                var list = db.vertificationCodes.Where(x => x.StartDate.Year == date.Year && x.StartDate.Month == date.Month && x.StartDate.Day == date.Day && x.hotelid == hotelid).ToList();
                var join = from j1 in list
                           join j2 in db.Hotel.ToList() on j1.hotelid equals j2.id
                           select new RoomCountResponse
                           {
                               HotelName = j2.HotelName,
                               RoomCount = list.Count
                           };
                return join.FirstOrDefault();
            }
        }

        public GetRoomOccupancyRateByDateResponse GetRoomOccupancyRateByDate(int hotelid, DateTime startDate)
        {
            using (var db = new DataContext())
            {
                var date1 = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);

                var allroomrate = db.Room.Count(x => x.hotelid == hotelid);
                var list = db.vertificationCodes.Where(x => x.StartDate.Year == startDate.Year && x.StartDate.Month == startDate.Month && x.StartDate.Day == startDate.Day && x.hotelid == hotelid).ToList();
                var join = from j1 in list
                           join j2 in db.Hotel.ToList() on j1.hotelid equals j2.id
                           select new GetRoomOccupancyRateByDateResponse
                           {
                               HotelName = j2.HotelName,
                               Occopancy = Convert.ToInt32((Convert.ToDecimal(list.Count) / Convert.ToDecimal(allroomrate)) * 100).ToString() + " " + "%"
                           };
                return join.First();
            }
        }

        public RoomCountResponse GetTotalRoomCountOfUsedApp(int hotelid)
        {
            using (var db = new DataContext())
            {
                var entity = new RoomCountResponse
                {
                    HotelName = db.Hotel.FirstOrDefault(x => x.id == hotelid).HotelName,
                    RoomCount = db.vertificationCodes.Where(x => x.hotelid == hotelid && x.isDeleted == false).ToList().Count
                };
                return entity;
            }
        }

        public RoomCountResponse GetTotalRoomCountOfUsedAppDateSearch(int hotelid, DateTime starDate, DateTime endDate)
        {
            using (var dB = new DataContext())
            {

                var data = dB.vertificationCodes.Where(x => x.hotelid == hotelid && x.StartDate >= starDate && x.StartDate <= endDate).ToList();
                var entityResponse = new RoomCountResponse
                {
                    HotelName = dB.Hotel.Where(x => x.id == hotelid).FirstOrDefault().HotelName,
                    RoomCount = data.Count
                };
                return entityResponse;
            }
        }
        /// <summary>
        /// Müşteri için telefonuna kod üretip ve gönderme ve database kaydetme
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseDto<VerificationCodeResponse> GetVertificationProduce(VerificationCodeRequest request)
        {
            using (var db = new DataContext())
            {
                var data = db.vertificationCodes.Where(x => x.hotelid == request.hotelid && x.roomid == request.roomid && x.IsFull == false).FirstOrDefault();

                if (data != null)
                {
                    return new ResponseDto<VerificationCodeResponse>().Fail(400, "Dolu");
                }
                var code = CodeRandomGeneration.RandomVertificationCodeCreate();

                //sms kodu atılacak

                request.Code = code;

                var result = Create(_mapper.Map<VerificationCodes>(request));

                var query = from d1 in db.Hotel
                            join d2 in db.Room on d1.id equals d2.hotelid
                            join d3 in db.vertificationCodes on d2.id equals d3.roomid
                            where d3.hotelid == d1.id && d3.Code == request.Code
                            select new VerificationCodeResponse
                            {
                                id = d3.id,
                                roomid = d2.id,
                                StartDate = d3.StartDate,
                                FinishDate = d3.FinishDate,
                                Code = d3.Code,
                                hotelid = d1.id,
                                HotelName = d1.HotelName,
                                Price = d3.Price,
                                RoomNo = d2.RoomNo.ToString(),
                                Phone = d3.Phone,
                            };

                return new ResponseDto<VerificationCodeResponse>().Success(query.FirstOrDefault(), 200);
            }
        }
    }
}
