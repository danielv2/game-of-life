using System.ComponentModel.DataAnnotations;

namespace GOF.Domain.Entities
{
    public class GameStageEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid GameId { get; set; }
        public int Generation { get; set; }
        public List<List<int>> Population { get; set; }
        public GameEntity Game { get; set; }
    }
}