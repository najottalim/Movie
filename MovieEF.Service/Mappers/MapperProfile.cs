using AutoMapper;
using MovieEF.Domain.Entities;
using MovieEF.Service.DTOs;
using MovieEF.Service.DTOs.Actors;
using MovieEF.Service.DTOs.Genres;
using MovieEF.Service.DTOs.Movies;

namespace MovieEF.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
		CreateMap<Actor, ActorForCreationDto>().ReverseMap();
		CreateMap<Movie, MovieForCreationDto>().ReverseMap();
		CreateMap<Genre, GenreForCreationDto>().ReverseMap();
		CreateMap<Movie, MovieViewDto>().ReverseMap();
    }
}