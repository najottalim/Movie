using System.ComponentModel.DataAnnotations.Schema;

namespace MovieEF.Domain.Entities;

public class MovieActor
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public long ActorId { get; set; }
    
    [ForeignKey(nameof(MovieId))] 
    public Movie? Movie { get; set; }
    
    [ForeignKey(nameof(ActorId))]
    public Actor? Actor { get; set; }
}