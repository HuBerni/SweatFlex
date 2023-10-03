
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SweatFlexAPI.Models;
using SweatFlexAPIClient;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexEF;
using SweatFlexEF.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        test2(args);

        //Console.ReadLine();

        //test();

        Console.ReadLine();
    }

    public async static void test()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClient = serviceProvider.GetService<IHttpClientFactory>();

        AuthService client = new(httpClient);

        var test = await client.LoginAsync(new SweatFlexData.DTOs.LoginDTO()
        {
            Email = "alexander@lbs4.com",
            Password = "test"
        });
        
        return;
    }

    //private async static void test()
    //{
    //    var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
    //    var httpClient = serviceProvider.GetService<IHttpClientFactory>();

    //    //var client = new UserService(httpClient);

    //    var test = await client.LoginAsync(new SweatFlexData.DTOs.LoginDTO()
    //    {
    //        Email = "abc",
    //        Password = "wtfisthis"
    //    });

    //    var test2 = await client.GetUsersAsync();

    //    await Console.Out.WriteLineAsync(test2.Result.ToString());
    //}

    private async static void test2(string[] args)
    {
        SweatFlexContextFactory cf = new SweatFlexContextFactory();
        DataHandler dh = new DataHandler(cf.CreateDbContext(args));

        var context = cf.CreateDbContext(args);

        var test = new SweatFlexContextProcedures(context);
        var output = new OutputParameter<int>();

        test.CreateUserAsync("10101", 1, "Sue", "Storm", "sue.Storm@yahoo.com", "PASSWORD", null);

        //var validationUser = await dh.LoginAsync("alexander@lbs4.com", "test");

        await Console.Out.WriteLineAsync("test");
    }
}