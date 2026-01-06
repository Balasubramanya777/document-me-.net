using DocumentMe.Repository.Repository.Base;
using DocumentMe.Service.IService.Base;
using DocumentMe.Utility.IUtility.Base;
using Scrutor;

namespace DocumentMe.API.Initiator
{
    public static class RegisterServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(RepositoryAssemblyMarker), typeof(ServiceAssemblyMarker), typeof(UtilityAssemblyMarker))
                .AddClasses(classes => classes.InNamespaces(RepositoryAssemblyMarker.NameSpace, ServiceAssemblyMarker.NameSpace, UtilityAssemblyMarker.NameSpace))
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}
