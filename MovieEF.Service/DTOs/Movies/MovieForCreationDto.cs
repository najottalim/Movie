namespace MovieEF.Service.DTOs.Movies;

public class MovieForCreationDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly MovieYear { get; set; }
    public string Duration { get; set; } = string.Empty;
    public DateOnly PremiereDate { get; set; } 
    
    public IEnumerable<long>? ActorIds { get; set; }
}