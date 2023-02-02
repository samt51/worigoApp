using Microsoft.AspNetCore.Http;
using System;

namespace Worigo.Core.Dtos.User.Dto
{
    public class ManagementUserAddOrUpdateRequest
    {
        /// <summary>
        /// user id
        /// </summary>
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
        public int? FloorNo { get; set; }
        public string phoneNumber { get; set; }
        public bool gender { get; set; }
        public DateTime? StartDateOfWork { get; set; }
        public DateTime? ExitEntryDate { get; set; }
        public int? hotelid { get; set; }
        public int departmanid { get; set; }
        public IFormFile? file { get; set; }

    }
}
