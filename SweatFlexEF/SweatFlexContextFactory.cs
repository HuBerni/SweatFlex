using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweatFlexEF.Models;

namespace SweatFlexEF
{
    public class SweatFlexContextFactory : IDesignTimeDbContextFactory<SweatFlexContext>
    {
        public SweatFlexContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("librarysettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<SweatFlexContext>();
            #pragma warning disable CS8604 // Possible null reference argument.
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            #pragma warning restore CS8604 // Possible null reference argument.


            return new SweatFlexContext(optionsBuilder.Options);
        }
    }
}
