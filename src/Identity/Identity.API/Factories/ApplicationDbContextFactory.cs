using Identity.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Identity.API.Factories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .AddUserSecrets(Assembly.GetAssembly(typeof(Startup)))
               .Build();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: i => i.MigrationsAssembly(typeof(Startup).Assembly.FullName));

            return new ApplicationDbContext(optionBuilder.Options);
        }

    }
}
