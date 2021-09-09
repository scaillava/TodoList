using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;

namespace Todo.Api.AutoMapper
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<UpsertTodoTaskViewModel, Domain.Models.TodoTask>();
            CreateMap<UpsertTodoViewModel, Domain.Models.Todo>()
                .ForMember(dest => dest.TodoTasks, act => act.MapFrom(src => src.TodoTasks));

            CreateMap<Domain.Models.Todo, TodoResponseViewModel>()
                .ForMember(dest => dest.TodoTasks, act => act.MapFrom(src => src.TodoTasks));
            CreateMap<Domain.Models.TodoTask, TodoTaskResponseViewModel>();
        }
    }
}
