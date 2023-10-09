using System.Net;

namespace SweatFlexAPIClient.APIModels
{
    /// <summary>
    /// A class to hold the response data for the API
    /// </summary>
    public class ApiResponse<T>
    {
        public ApiResponse() => ErrorMessages = new List<string>();

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public T? Result { get; set; }
    }
}
