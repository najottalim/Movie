using MovieEF.Domain.Entities;

namespace MovieEF.Service.DTOs.Movies;

public class MovieViewDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly MovieYear { get; set; }
    public string Duration { get; set; } = string.Empty;
    public DateOnly PremiereDate { get; set; } 
    
    public IEnumerable<Actor>? Actors { get; set; }
}