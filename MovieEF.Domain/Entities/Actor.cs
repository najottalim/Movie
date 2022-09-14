namespace MovieEF.Domain.Entities;

public class Actor
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Hobby { get; set; } = string.Empty;
    public bool Gender { get; set; }
    public DateOnly BirthDate { get; set; }
}