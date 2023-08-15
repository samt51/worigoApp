using System.Collections.Generic;
using System.Text.Json.Serialization;
using Worigo.Core.Enum;

namespace Worigo.Core.Dtos.ResponseDtos
{
    public class ResponseDto<T>
    {
        public T data { get; set; }

        public int statusCode { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }
        public ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { data = data, statusCode = 200, IsSuccessful = true, message = MessageEnum.IsSuccess };
        }
        public ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { statusCode = 200, IsSuccessful = true, message = MessageEnum.IsSuccess };
        }
        public ResponseDto<T> Fail(int statusCode, List<string> errormessages)
        {
            return new ResponseDto<T> { data = default, statusCode = 400, IsSuccessful = false, errors = errormessages, message = MessageEnum.IsFailed, };
        }
        public ResponseDto<T> Fail(int statusCode, string errors)
        {
            var error = new List<string>();
            error.Add(errors);
            return new ResponseDto<T> { statusCode = 400, IsSuccessful = false, errors = error, message = MessageEnum.IsFailed };
        }
        public ResponseDto<T> Authorization()
        {
            return new ResponseDto<T> { statusCode = 401, IsSuccessful = false, message = MessageEnum.AuthorizationFail, };
        }

    }
}
