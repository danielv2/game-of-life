using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOF.Domain.Models.GameModel.Request;

namespace GOF.Domain.Models.GameModel.Validators
{
    public class PostGameRequestValidator : GameModelValidatorBase<PostGameRequest>
    {
        public PostGameRequestValidator() : base()
        {
        }
    }
}