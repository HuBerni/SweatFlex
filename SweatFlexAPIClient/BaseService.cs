using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using SweatFlexAPIClient.APIModels;
using SweatFlexAPIClient.Enum;
using System.Net.Http.Headers;
using System.Text;


namespace SweatFlexAPIClient
{
    public class BaseService<T> : IBaseService<T>
    {
        protected string? SweatFlexURL;
        public IHttpClientFactory httpClient { get; set; }
        public BaseService()
        {
            httpClient = HttpClientFactory.CreateFactory();
            this.httpClient = httpClient;
        }
        public async Task<ApiResponse<T>> SendAsync<T>(ApiRequest apiRequest)
        {

        ApiResponse<T> responseModel = new();

            try
            {
                if (!String.IsNullOrEmpty(TokenStorage.Token))
                {
                    apiRequest.Token = TokenStorage.Token;
                }

                var client = httpClient.CreateClient("SweatFlexAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    ApiResponse<T> ApiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(apiContent);
                    if (ApiResponse != null && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<ApiResponse<T>>(res);
                        return returnObj;
                    }
                }
                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(apiContent);
                return APIResponse;

            }
            catch (Exception e)
            {
                var dto = new ApiResponse<T>
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(res);
                return APIResponse;
            }
        }
    }
}
