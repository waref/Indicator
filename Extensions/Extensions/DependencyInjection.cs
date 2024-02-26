
using Depences.Application.IMangers;
using Depences.Application.Managers;
using Depences.Domain.Models;
using Depences.Extensions.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Depences.Extensions.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureManagers(this IServiceCollection services,
                                                  IConfiguration Configuration,
                                                  bool overrideDBConfig)
        {
            services.ConfigureRepositories(Configuration, overrideDBConfig);

            #region Depences
            services.AddScoped<IDepenceManager <Depence>,DepenceManager >();
            #endregion

            services.AddAutoMapper(typeof(BusinessMapperConfiguration));
        }
    }
}
