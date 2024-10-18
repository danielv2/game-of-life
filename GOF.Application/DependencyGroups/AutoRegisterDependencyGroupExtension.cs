using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GOF.Application.DependencyGroups
{
    /// <summary>
    /// Interface for dependency group registration dynamically
    /// </summary>
    public static class AutoRegisterDependencyGroupExtension
    {
        /// <summary>
        /// Register all dependency groups from assemblies
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        public static void RegisterDependencyGroupFromAssemblies(this IServiceCollection serviceCollection)
        {
            var serviceDependencyType = typeof(IDependencyGroup);
            var serviceDependencies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => serviceDependencyType.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            serviceDependencies.ForEach(type =>
            {
                var instance = (IDependencyGroup)Activator.CreateInstance(type);
                instance.Register(serviceCollection);
            });
        }
    }
}