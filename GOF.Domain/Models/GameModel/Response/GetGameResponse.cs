using GOF.Domain.Models.GameStageModel;
using GOF.Domain.Models.GameStageModel.Response;

namespace GOF.Domain.Models.GameModel.Response
{
    /// <summary>
    /// GetGameResponse class
    /// </summary>
    public class GetGameResponse : GameModelBase
    {
        public Guid Id { get; set; }
        public ICollection<GetGameStageResponse>? Stages { get; set; }
    }
}