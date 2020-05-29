using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {

        }

        public  GameDbContext()
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<GameGenreConnection> GameGenreConnection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(game =>
            {

                game
                    .Property(g => g.Title)
                    .IsRequired(true);

                game
                    .Property(g => g.Price)
                    .IsRequired(true);

                game
                   .Property(g => g.Description)
                   .IsRequired(true);

                game
                   .Property(g => g.Platform)
                   .IsRequired(true);


                game
                    .HasOne(g => g.Manufacturer)
                    .WithMany(m => m.Games)
                    .HasForeignKey(g => g.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Genre>(genre =>
            {

                genre
                    .Property(g => g.Name)
                    .IsRequired(true);
                
            });


            modelBuilder.Entity<Manufacturer>(manufacturer =>
            {
                manufacturer
                    .Property(m => m.Name)
                    .IsRequired(true);
            });

            modelBuilder.Entity<GameGenreConnection>(entity =>
            {

                entity.HasKey(entity => new { entity.GameId, entity.GenreId });

                entity
                    .HasOne(e => e.Game)
                    .WithMany(g => g.GameGenres)
                    .HasForeignKey(e => e.GameId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity
                   .HasOne(e => e.Genre)
                   .WithMany(ge => ge.GameGenres)
                   .HasForeignKey(e => e.GenreId)
                   .OnDelete(DeleteBehavior.Restrict);

            });
    }

    }
}
