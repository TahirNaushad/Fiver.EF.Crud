using Fiver.EF.Crud.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fiver.EF.Crud
{
    public class Database : DbContext
    {
        public Database(
            DbContextOptions<Database> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                        .Property(actor => actor.Timestamp)
                        .ValueGeneratedOnAddOrUpdate()
                        .IsConcurrencyToken();
        }
    }
}
