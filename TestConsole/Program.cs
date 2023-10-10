using Microsoft.Extensions.DependencyInjection;
using SweatFlexAPI.Models;
using SweatFlexAPIClient;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;

test();
Console.ReadLine();

async static void test()
{
    var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
    var httpClient = serviceProvider.GetService<IHttpClientFactory>();

    var client = new AuthService(httpClient);

    client.LoginAsync(new LoginDTO()
    {
        Email = "asdf",
        Password = "asdf"
    });
}