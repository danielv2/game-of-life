using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GOF.Application.DependencyGroups
{
    public interface IDependencyGroup
    {
        void Register(IServiceCollection serviceCollection);
    }
}