using System.Text.Json;
using GOF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GOF.Infra.Context
{
    /// <summary>
    /// SQLiteDbContext class
    /// </summary>
    public class SQLiteDbContext : DbContext
    {
        public DbSet<GameEntity> GameEntity { get; set; }
        public DbSet<GameStageEntity> GameStages { get; set; }

        public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// OnModelCreating method to configure the models and relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<List<List<int>>, string>(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<List<int>>>(v, new JsonSerializerOptions())
            );

            modelBuilder.Entity<GameEntity>()
                .Property(e => e.InitialState)
                .HasConversion(converter);

            modelBuilder.Entity<GameStageEntity>()
                .Property(e => e.Population)
                .HasConversion(converter);

            modelBuilder.Entity<GameStageEntity>()
                .HasOne(gs => gs.Game)
                .WithMany(g => g.Stages)
                .HasForeignKey(gs => gs.GameId);

            base.OnModelCreating(modelBuilder);
        }
    }
}