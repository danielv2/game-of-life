using GOF.Application.DependencyGroups;
using GOF.Host.DependencyGroups;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GOF.IoC.DependencyGroups
{
    /// <summary>
    /// VersioningDependencies class that implements IDependencyGroup
    /// </summary>
    /// <seealso cref="IDependencyGroup" />
    /// <remarks>
    /// This class is used to register all dependencies related to the Versioning entity
    /// </remarks>
    public class VersioningDependencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
        }
    }
}