using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GOF.Host.DependencyGroups
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class SwaggerConfiguration : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerConfiguration"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public SwaggerConfiguration(IApiVersionDescriptionProvider provider) => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.GroupName,
                Title = "Game.Of.Life API",
                Description = "Game.Of.Life API",
                TermsOfService = new Uri("https://github.com/"),
                Contact = new OpenApiContact
                {
                    Name = "Daniel Vieira",
                    Email = "danielvieira.dev@gmail.com",
                    Url = new Uri("https://github.com/danielv2"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under MIT License",
                    Url = new Uri("https://github.com/"),
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}