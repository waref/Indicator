using Depences.Infrastructure.DBContext;
using Depences.Infrastructure.Implementation.Repositories;
using Depences.Infrastructure.Interfaces.IRepositories;
using Depences.Infrastructure.Static;
using Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Depences.Extensions.Infrastructure
{
    public static class DependencyInjection
    {
        private readonly static IConfigurationRoot _configuration;
        static DependencyInjection()
        {

            var currentProjectPath = Directory.GetParent(Environment.CurrentDirectory) + "\\Depences.Infrastructure\\";
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(path: currentProjectPath+"dbsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: currentProjectPath+$"dbsettings.{WebHostEnvironment.CurrentWebHostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
        }
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration, bool overrideDBConfig)
        {
            var connectionString = overrideDBConfig ? configuration.GetConnectionString(DbConstants.DefaultConnectionString) :
                                                        _configuration.GetConnectionString(DbConstants.DefaultConnectionString);
          

            services.AddDbContext<DefaultDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging(true);
            });

            #region Depence
            services.AddScoped<IDepenceRepository, DepenceRepository>();
            #endregion
        }

    }
}
