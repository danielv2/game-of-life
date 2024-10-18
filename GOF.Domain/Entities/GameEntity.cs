using System.ComponentModel.DataAnnotations;

namespace GOF.Domain.Entities
{
    public class GameEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SquareSideSize { get; set; }
        public List<List<int>>? InitialState { get; set; }
        public int maxGenerations { get; set; }
        public ICollection<GameStageEntity>? Stages { get; set; }
    }
}