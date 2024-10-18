using AutoMapper;
using GOF.Application.DependencyGroups;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Mapping;
using GOF.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
    public class GameStageDependencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            // Validators

            // Mappings
            services.AddAutoMapper(typeof(GameStageMapping));

            // Services

            // Repositories
            services.AddScoped<IGameStageRepository, GameStageRepository>();
        }
    }
}