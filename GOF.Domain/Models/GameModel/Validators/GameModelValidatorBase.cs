using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace GOF.Domain.Models.GameModel.Validators
{
    public abstract partial class GameModelValidatorBase<TModel> : AbstractValidator<TModel>
           where TModel : GameModelBase
    {
        public GameModelValidatorBase()
        {
            ConfigureValidation();
        }

        protected virtual void ConfigureValidation()
        {
            RuleFor(c => c)
                .NotNull();

            RuleFor(c => c.SquareSideSize)
                .GreaterThan(10)
                .WithMessage("SquareSideSize must be greater than 10");

            RuleFor(c => c.maxGenerations)
                .GreaterThan(0)
                .WithMessage("maxGenerations must be greater than 0");
        }
    }
}