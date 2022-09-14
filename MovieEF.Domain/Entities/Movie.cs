
namespace MovieEF.Domain.Entities;

public class Movie
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly MovieYear { get; set; }
    public string Duration { get; set; } = string.Empty;
    public DateOnly PremiereDate { get; set; } 
}