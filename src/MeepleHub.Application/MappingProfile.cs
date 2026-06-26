using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MeepleHub.Application.DTOs;
using MeepleHub.Domain.Entities;

namespace MeepleHub.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserGame, UserGameDto>().ReverseMap();
            CreateMap<Loan, LoanDto>().ReverseMap();
        }
    }
}
