using System;
using System.IO;
using System.Reflection;
using GOF.Application.DependencyGroups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
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