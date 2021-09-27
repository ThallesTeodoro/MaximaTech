using System;
using Newtonsoft.Json;

namespace MaximaTech.Web.Helpers
{
    public class JsonResponse<TData, TErrors>
    {
        /// <summary>
        /// Response message
        /// </summary>
        [JsonProperty("message")]
        public String Message { get; set; }

        /// <summary>
        /// Response status code
        /// </summary>
        [JsonProperty("statusCode")]
        public Int32 StatusCode { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        [JsonProperty("data")]
        public TData Data { get; set; }

        /// <summary>
        /// Response validation errors
        /// </summary>
        [JsonProperty("errors")]
        public TErrors Errors { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="statusCode">Response status code</param>
        /// <param name="message">Response message</param>
        public JsonResponse(Int32 statusCode = 200, String message = "Ok")
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        /// <summary>
        /// Convert class to json
        /// </summary>
        /// <returns>Json string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
