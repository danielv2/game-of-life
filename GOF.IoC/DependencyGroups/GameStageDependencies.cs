using AutoMapper;
using GOF.Application.DependencyGroups;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Mapping;
using GOF.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
    /// <summary>
    /// GameStageDependencies class that implements IDependencyGroup
    /// </summary>
    /// <seealso cref="IDependencyGroup" />
    /// <remarks>
    /// This class is used to register all dependencies related to the GameStage entity
    /// </remarks>
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