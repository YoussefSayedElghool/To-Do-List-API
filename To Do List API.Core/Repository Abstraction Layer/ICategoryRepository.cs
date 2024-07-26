using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Core.Repository_Abstraction_Layer
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        Task<QueryResultDto<List<Category>>> GetAllAsync();
        Task<QueryResultDto<Category>> GetByIdAsync(int id);
        Task<QueryResultDto<Category>> InsertAsync(Category item);
        Task<QueryResultDto<Category>> EditAsync(Category item);
        Task<QueryResultDto<Category>> DeleteAsync(int id);
    }
}
