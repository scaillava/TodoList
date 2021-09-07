using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.App.ViewModels;

namespace Todo.App.AutoMapper
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<UpsertTodoTaskViewModel, Domain.Models.TodoTask>();
            CreateMap<UpsertTodoViewModel, Domain.Models.Todo>()
                .ForMember(dest => dest.TodoChecks, act => act.MapFrom(src => src.TodoChecks));
        }
    }
}
