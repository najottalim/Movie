using Microsoft.EntityFrameworkCore;
using MovieEF.Domain.Entities;

namespace MovieEF.Data.Contexts;

public class MovieEFDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Host=localhost;Port=5432;User Id=postgres;Database=movieMovie;Password=root";

        optionsBuilder.UseNpgsql(connectionString);
    }

    public virtual DbSet<Actor> Actors { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<MovieActor> MovieActors { get; set; }
    public virtual DbSet<MovieGenre> MovieGenres { get; set; }
}