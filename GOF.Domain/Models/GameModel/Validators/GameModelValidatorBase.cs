using FluentValidation;

namespace GOF.Domain.Models.GameModel.Validators
{
    /// <summary>
    /// GameModelValidatorBase class
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract partial class GameModelValidatorBase<TModel> : AbstractValidator<TModel>
           where TModel : GameModelBase
    {
        public GameModelValidatorBase()
        {
            ConfigureValidation();
        }

        /// <summary>
        /// ConfigureValidation method to be implemented by the GameModel class
        /// </summary>
        protected virtual void ConfigureValidation()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.SquareSideSize)
                .GreaterThan(4)
                .WithMessage("SquareSideSize must be greater than 4");

            RuleFor(c => c.MaxGenerations)
                .GreaterThan(0)
                .WithMessage("maxGenerations must be greater than 0");
        }
    }
}