using AutoMapper;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Profiles
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();

            CreateMap<QueryResultDto<ToDo>, QueryResultDto<ToDoDto>>().ReverseMap();
            CreateMap<QueryResultDto<List<ToDo>>, QueryResultDto<List<ToDoDto>>>().ReverseMap();
        }
    }
}
