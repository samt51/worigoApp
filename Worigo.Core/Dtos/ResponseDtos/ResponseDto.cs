using System.Collections.Generic;
using System.Text.Json.Serialization;
using Worigo.Core.Enum;

namespace Worigo.Core.Dtos.ResponseDtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }
        
        public int StatusCode { get; private set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode,  IsSuccessful = true ,Message=MessageEnum.IsSuccess};
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> {  StatusCode = statusCode,  IsSuccessful = true, Message = MessageEnum.IsSuccess };
        }
        public static ResponseDto<T> Fail(int statusCode, List<string> errormessages)
        {
            return new ResponseDto<T> { Data = default, StatusCode = statusCode,   IsSuccessful = false,Errors=errormessages, Message = MessageEnum.IsFailed, };
        }
        public static ResponseDto<T> Fail(int statusCode, string errors)
        {
            var error = new List<string>();
            error.Add(errors);
            return new ResponseDto<T> { StatusCode = statusCode, IsSuccessful = false, Errors = error, Message = MessageEnum.IsFailed };
        }
        public static ResponseDto<T> Authorization()
        {
            return new ResponseDto<T> {   StatusCode = 401, IsSuccessful = false,  Message = MessageEnum.AuthorizationFail, };
        }
    }
}
