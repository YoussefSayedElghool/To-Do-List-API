using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Repository.abstraction_layer
{
    public interface IToDoRepository
    {
        Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId);
        Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId ,int CategoryId);
        Task<QueryResultDto<ToDo>> GetByIdAsync(int id);
        Task<QueryResultDto<ToDo>> InsertAsync(ToDo item);
        Task<QueryResultDto<ToDo>> EditAsync(ToDo item);
        Task<QueryResultDto<ToDo>> DeleteAsync(int id);
    }
}
