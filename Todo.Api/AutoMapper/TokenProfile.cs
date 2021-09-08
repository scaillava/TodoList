using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;

namespace Todo.Api.AutoMapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<Domain.Models.UserToken, TokenViewModel>()
                .ForMember(dest => dest.AuthToken, act => act.MapFrom(src => src.Token));
        }

    }
}
