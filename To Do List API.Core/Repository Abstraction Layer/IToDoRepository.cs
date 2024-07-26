using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Repository.abstraction_layer
{
    public interface IToDoRepository : IBaseRepository<ToDo>
    {
        Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId);
        Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId ,int CategoryId);

    }
}
