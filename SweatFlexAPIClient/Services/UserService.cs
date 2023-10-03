//using Microsoft.Extensions.Configuration;
//using SweatFlexAPI.Models;
//using SweatFlexAPIClient.Interface;
//using SweatFlexData.DTOs.Create;
//using SweatFlexData.DTOs.Update;

//namespace SweatFlexAPIClient.Services
//{
//    public class UserService : BaseService, IUserService
//    {
//        string _suffix;
//        public UserService(IHttpClientFactory httpClient) : base(httpClient)
//        {
//            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
//            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
//            _suffix = "UserAPI";
//        }

//        public async Task<ApiResponse> GetUsersAsync()
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}"
//            });
//        }

//        public async Task<ApiResponse> GetUserAsync(string id)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}/{id}"
//            });
//        }

//        public async Task<ApiResponse> GetUserByCoachAsync(string coachId)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}/{coachId}"
//            });
//        }

//        public async Task<ApiResponse> CreateUserAsync(UserCreateDTO createDTO)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.PUT,
//                Url = $"{SweatFlexURL}{_suffix}",
//                Data = createDTO
//            });
//        }

//        public async Task<ApiResponse> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.PUT,
//                Url = $"{SweatFlexURL}{_suffix}",
//                Data = updateDTO
//            });
//        }

//        public async Task<ApiResponse> DeleteUserAsync(string id)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.DELETE,
//                Url = $"{SweatFlexURL}{_suffix}/{id}"
//            });
//        }

//        //TODO: maybe needs to be overworked
//        public async Task<ApiResponse> SetUserInactiveAsync(string id)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.PUT,
//                Url = $"{SweatFlexURL}{_suffix}",
//                Data = id
//            });
//        }

//    }
//}
