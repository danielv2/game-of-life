using System;
using System.IO;
using System.Reflection;
using GOF.Application.DependencyGroups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
    /// <summary>
    /// SwaggerDependencies class that implements IDependencyGroup
    /// </summary>
    /// <seealso cref="IDependencyGroup" />
    /// <remarks>
    /// This class is used to register all dependencies related to the Swagger entity
    /// </remarks>
    public class SwaggerDependencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            services.Configure<
            ApiBehaviorOptions>(options =>
            {
                options.SuppressInferBindingSourcesForParameters = false;

            });

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}