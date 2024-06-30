using AutoMapper;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;
using To_Do_List_API.Service.abstraction_layer;

namespace To_Do_List_API.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository toDoRepository;
        private readonly IMapper mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            this.toDoRepository = toDoRepository;
            this.mapper = mapper;
        }

        public async Task<QueryResultDto<List<ToDoDto>>> GetToDoesAsync(string userId, int CategoryId)
        {
            return mapper.Map<QueryResultDto<List<ToDoDto>>>(await toDoRepository.GetAllByAsync(userId, CategoryId));
        }
        
        public async Task<QueryResultDto<List<ToDoDto>>> GetToDoAsync(int toDoId)
        {
            return mapper.Map<QueryResultDto<List<ToDoDto>>>(await toDoRepository.GetByIdAsync(toDoId));
        }

        public async Task<QueryResultDto<ToDoDto>> AddToDoAsync(ToDoDto item, string userId)
        {
            var toDo = mapper.Map<ToDo>(item);
            toDo.UserId = userId;
            var result = await toDoRepository.InsertAsync(toDo);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }

        public async Task<QueryResultDto<ToDoDto>> EditToDoAsync(ToDoDto item , string userId)
        {
            var toDo = mapper.Map<ToDo>(item);
            toDo.UserId = userId;

            var result = await toDoRepository.EditAsync(toDo);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }

        public async Task<QueryResultDto<ToDoDto>> RemoveToDoAsync(int id)
        {
            var result = await toDoRepository.DeleteAsync(id);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }
    }
}
