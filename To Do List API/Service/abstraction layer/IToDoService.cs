using To_Do_List_API.DTO;

namespace To_Do_List_API.Service.abstraction_layer
{
    public interface IToDoService
    {
        Task<QueryResultDto<List<ToDoDto>>> GetToDoesAsync(string userId, int CategoryId);
        Task<QueryResultDto<List<ToDoDto>>> GetToDoAsync(int toDoId);
        Task<QueryResultDto<ToDoDto>> AddToDoAsync(ToDoDto item, string userId);
        Task<QueryResultDto<ToDoDto>> EditToDoAsync(ToDoDto item , string userId);
        Task<QueryResultDto<ToDoDto>> RemoveToDoAsync(int id);
    }
}
