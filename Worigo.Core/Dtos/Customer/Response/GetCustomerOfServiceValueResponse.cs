using System;
using System.Collections.Generic;
using Worigo.Core.Dtos.ServiceOfValueDto.Response;

namespace Worigo.Core.Dtos.Customer.Response
{
    public class GetCustomerOfServiceValueResponse
    {
        public string FullName { get; set; }
        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public int VertificationId { get; set; }
        public List<ServiceOfValueResponse> serviceOfValueResponses{ get; set; }
    }
}
