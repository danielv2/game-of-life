using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOF.Domain.Models.GameStageModel.Response
{
    public class GetGameStageResponse : GameStageModelBase
    {
        public Guid Id { get; set; }
    }
}