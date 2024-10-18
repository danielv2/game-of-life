using AutoMapper;
using FluentValidation;
using GOF.Application.DependencyGroups;
using GOF.Domain.Interfaces.Repositories;
using GOF.Domain.Interfaces.Services;
using GOF.Domain.Mapping;
using GOF.Domain.Models.GameModel.Request;
using GOF.Domain.Models.GameModel.Validators;
using GOF.Infra.Repositories;
using GOF.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GOF.IoC.DependencyGroups
{
    /// <summary>
    /// GameDependencies class that implements IDependencyGroup
    /// </summary>
    /// <seealso cref="IDependencyGroup" />
    /// <remarks>
    /// This class is used to register all dependencies related to the Game entity
    /// </remarks>
    public class GameDependencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            // Validators
            services.AddTransient<IValidator<PostGameRequest>, PostGameRequestValidator>();

            // Mappings
            services.AddAutoMapper(typeof(GameMapping));

            // Services
            services.AddScoped<IGameService, GameService>();

            // Repositories
            services.AddScoped<IGameRepository, GameRepository>();
        }
    }
}