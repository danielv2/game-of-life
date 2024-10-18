namespace GOF.Domain.Models.GameStageModel
{
    public class GameStageModelBase
    {
        public Guid GameId { get; set; }
        public int Generation { get; set; }
        public List<List<int>> Population { get; set; }
    }
}