using Microsoft.Extensions.Configuration;
using SweatFlexAPI.Models;
using SweatFlexAPIClient.Interface;
using SweatFlexData.DTOs.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient
{
    public class UserService : BaseService, IUserService
    {
        string _sweatFlexURL;
        
        public UserService(IHttpClientFactory httpClient) : base(httpClient)
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            _sweatFlexURL = config.GetValue<string>("URL:SweatFlexRestAPI");
        }

        public Task<T> Register<T>(UserCreateDTO createDTO)
        {
            
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = Enum.ApiType.POST,
                Data = createDTO,
                Token = null,
                Url = _sweatFlexURL + "UserAPI"
            });
        }

    }
}
