using System.ComponentModel.DataAnnotations;

namespace MovieEF.Service.DTOs.Actors;

public class ActorForCreationDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Hobby { get; set; }
    
    [Required]
    public bool Gender { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
}