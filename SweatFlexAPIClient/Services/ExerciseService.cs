//using Microsoft.Extensions.Configuration;
//using SweatFlexAPI.Models;
//using SweatFlexAPIClient.Interface;
//using SweatFlexData.DTOs;
//using SweatFlexData.DTOs.Create;
//using SweatFlexData.DTOs.Update;
//using SweatFlexData.Interface.IDTOs;

//namespace SweatFlexAPIClient.Services
//{
//    public class ExerciseService : BaseService<IExerciseDTO>, IExerciseService
//    {
//        string _suffix;
//        public ExerciseService(IHttpClientFactory httpClient) : base(httpClient)
//        {
//            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
//            SweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
//            _suffix = "ExerciseAPI";
//        }

//        public async Task<IList<ExerciseDTO>> GetExercisesAsync()
//        {
//            var result = await SendAsync <IList<ExerciseDTO>>(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}"
//            });

//            if (!result.IsSuccess)
//            {
//                return null;
//            }

//            return result.Result;
//        }

//        public async Task<IList<ExerciseDTO>> GetExercisesAsync(string id)
//        {
//            var result = await SendAsync<IList<ExerciseDTO>>(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}/user/{id}"
//            });

//            return result.Result;            
//        }

//        public async Task<ExerciseDTO> GetExerciseAsync(int id)
//        {
//            var result = await SendAsync<ExerciseDTO>(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.GET,
//                Url = $"{SweatFlexURL}{_suffix}/{id}"
//            });

//            return result.Result;
//        }

//        public async Task<ApiResponse> CreateExerciseAsync(ExerciseCreateDTO createDTO)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.POST,
//                Url = $"{SweatFlexURL}{_suffix}",
//                Data = createDTO
//            });
//        }

//        public async Task<ApiResponse> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.POST,
//                Url = $"{SweatFlexURL}{_suffix}",
//                Data = updateDTO
//            });
//        }

//        public async Task<ApiResponse> DeleteExerciseAsync(int id)
//        {
//            return await SendAsync(new ApiRequest()
//            {
//                ApiType = Enum.ApiType.DELETE,
//                Url = $"{SweatFlexURL}{_suffix}/{id}"
//            });
//        }
//    }
//}
