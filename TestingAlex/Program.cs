using Microsoft.Extensions.DependencyInjection;
using SweatFlexAPIClient.Services;
using SweatFlexData.DTOs.Update;
using SweatFlexEF;
using SweatFlexEF.Models;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        //test2(args);

        //Console.ReadLine();

        test();

        Console.ReadLine();
    }

    public async static void test()
    {
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var httpClient = serviceProvider.GetService<IHttpClientFactory>();

        ExerciseService client = new(httpClient);
        AuthService auth = new(httpClient);

        var user = await auth.LoginAsync(new SweatFlexData.DTOs.LoginDTO()
        {
            Email = "henry@yahoo.com",
            Password = "12345"
        });

        var test = await client.UpdateExerciseAsync(2, new ExerciseUpdateDTO()
        {
            Creator = "10102",
            Description = "big chest for big men",
            Equipment = 3,
            Musclegroup = 1,
            Name = "Bench Press",
            Type = 1
        });

        await Console.Out.WriteLineAsync("somsing");

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

        var test2 = await test.CreateUserAsync("10102", 2, "Henry", "Jones", "henry@yahoo.com", "12345", null, output);

        //var validationUser = await dh.LoginAsync("alexander@lbs4.com", "test");

        await Console.Out.WriteLineAsync("test");
    }
}