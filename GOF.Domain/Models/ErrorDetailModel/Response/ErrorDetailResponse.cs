using System.Collections.Generic;
using Newtonsoft.Json;

namespace GOF.Domain.Models.ErrorDetailModel.Response
{
    /// <summary>
    /// ErrorDetailResponse class
    /// </summary>
    public class ErrorDetailResponse
    {
        public ErrorDetailResponse()
        {
            this.messages = new List<string>();
        }
        public int statusCode { get; set; }
        public string errorCode { get; set; }
        public List<string> messages { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ErrorDetailResponse Create() => new ErrorDetailResponse { };

        public static ErrorDetailResponse Create(int statusCode, string errorCode, List<string> messages) => new ErrorDetailResponse
        {
            statusCode = statusCode,
            errorCode = errorCode,
            messages = messages
        };

        public void AddMessage(string message)
        {
            this.messages.Add(message);
        }
    }
}