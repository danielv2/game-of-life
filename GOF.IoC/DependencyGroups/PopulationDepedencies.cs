using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Application.DependencyGroups;
using GOF.Domain.Interfaces.Services;
using GOF.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
    /// <summary>
    /// PopulationDependencies class that implements IDependencyGroup
    /// </summary>
    /// <seealso cref="IDependencyGroup" />
    /// <remarks>
    /// This class is used to register all dependencies related to the Population entity
    /// </remarks>
    public class PopulationDepedencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            //Services
            services.AddScoped<IPopulationService, PopulationService>();
        }
    }
}