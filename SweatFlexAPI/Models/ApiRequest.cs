using SweatFlexAPIClient.Enum;

namespace SweatFlexAPI.Models
{
    /// <summary>
    /// A class to hold the request data for the API
    /// </summary>
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
