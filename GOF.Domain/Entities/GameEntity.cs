using System.ComponentModel.DataAnnotations;

namespace GOF.Domain.Entities
{
    /// <summary>
    /// GameEntity class
    /// </summary>
    /// <remarks>
    /// This class represents a game of life core entity.
    /// </remarks>
    public class GameEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SquareSideSize { get; set; }
        public List<List<int>>? InitialState { get; set; }
        public int MaxGenerations { get; set; }
        public ICollection<GameStageEntity>? Stages { get; set; }
    }
}