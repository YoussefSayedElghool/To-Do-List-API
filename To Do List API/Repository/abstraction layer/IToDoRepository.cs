using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Repository.abstraction_layer
{
    public interface IToDoRepository
    {
        Task<QueryResultDto<List<ToDo>>> GetAllBy(string userId);
        Task<QueryResultDto<List<ToDo>>> GetAllBy(string userId ,int CategoryId);
        Task<QueryResultDto<ToDo>> GetById(int id);
        Task<QueryResultDto<ToDo>> Insert(ToDo item);
        QueryResultDto<ToDo> Edit(ToDo item);
        Task<QueryResultDto<ToDo>> Delete(int id);
    }
}
