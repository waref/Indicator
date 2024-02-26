using Depences.Extensions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Depences.Application;
using System.Reflection;


namespace Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services
                                        , IConfiguration Configuration
                                        , bool overrideDBConfig = false
                                            )
        {
          services.ConfigureManagers(Configuration, overrideDBConfig);
        }

    }
}
