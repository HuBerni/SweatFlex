using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SweatFlexEF.Models;

namespace SweatFlexEF
{
    public class SweatFlexContextFactory : IDesignTimeDbContextFactory<SweatFlexContext>
    {

        /// <summary>
        /// This Class is used to create a context with a ConnectionString from a config file
        /// </summary>
        /// <param name="args">This param is needed for convention but not used in our case</param>
        /// <returns></returns>
        public SweatFlexContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("librarysettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<SweatFlexContext>();
#pragma warning disable CS8604 // Possible null reference argument.
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:Azure"]);
#pragma warning restore CS8604 // Possible null reference argument.


            return new SweatFlexContext(optionsBuilder.Options);
        }
    }
}
