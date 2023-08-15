namespace Worigo.Core.Dtos.VerificationCodeDto.Request
{
    public class ForAccessRequestModel
    {
        public string Code { get; set; }
        public string Phone { get; set; }
        public string DeviceId { get; set; }
        public string Lng { get; set; }

    }
}
