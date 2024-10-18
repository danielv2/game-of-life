namespace GOF.Domain.Models.GameStageModel
{
    /// <summary>
    /// GameStageModelBase class to be used as a base for GameStageModel
    /// </summary>
    public class GameStageModelBase
    {
        public Guid GameId { get; set; }
        public int Generation { get; set; }
        public List<List<int>> Population { get; set; }
    }
}