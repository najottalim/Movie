using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEF.Domain.Entities;

public class MovieGenre
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public long GenreId { get; set; }
    
    [ForeignKey(nameof(MovieId))]
    public Movie? Movie { get; set; }
    
    [ForeignKey(nameof(GenreId))]
    public Genre? Genre { get; set; }
}