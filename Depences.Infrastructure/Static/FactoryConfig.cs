using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Depences.Infrastructure.DBContext;

namespace Depences.Infrastructure.Static
{
    public static class FactoryConfig
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path: "dbsettings.json", optional: true, reloadOnChange: true)
    .Build();

        private static string DefaultConnectionString => Configuration.GetConnectionString(DbConstants.DefaultConnectionString);
        public class DefaultContextFactory : IDesignTimeDbContextFactory<DefaultDbContext>
        {
            public DefaultDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<DefaultDbContext>();
                builder.UseSqlServer(DefaultConnectionString);

                return new DefaultDbContext(builder.Options);
            }
        }
    }
}
