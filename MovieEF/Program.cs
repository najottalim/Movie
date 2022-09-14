using MovieEF.Domain.Entities;
using MovieEF.Service.DTOs.Movies;
using MovieEF.Service.Interfaces;
using MovieEF.Service.Services;

IMovieService movieService = new MovieService();

var movieViewDto = await movieService.CreateAsync(new MovieForCreationDto()
{
    Name = "Xorlanganlar 3", Duration = "2:05",
    MovieYear = DateOnly.Parse("2023/09/15"),
    PremiereDate = DateOnly.Parse("2023/09/15"),
    ActorIds = new long[] {2, 1}
});

Console.WriteLine($"{movieViewDto.Id} {movieViewDto.Name}\n\tActors");

foreach (var actor in movieViewDto.Actors!)
{
    Console.WriteLine($"{actor.Id} {actor.FirstName} {actor.LastName}");
}

