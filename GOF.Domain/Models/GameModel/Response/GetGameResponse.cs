using GOF.Domain.Models.GameStageModel;
using GOF.Domain.Models.GameStageModel.Response;

namespace GOF.Domain.Models.GameModel.Response
{
    public class GetGameResponse : GameModelBase
    {
        public Guid Id { get; set; }
    }
}