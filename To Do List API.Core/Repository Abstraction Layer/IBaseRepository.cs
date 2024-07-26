
using System.Linq.Expressions;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Core.Repository_Abstraction_Layer;
public interface IBaseRepository<T> where T : class
{
    Task<QueryResultDto<List<T>>> GetAllAsync();
    Task<QueryResultDto<T>> GetByIdAsync(int id);
    Task<QueryResultDto<List<T>>> GetWhereAsync(Expression<Func<T, bool>> criteria);
    Task<QueryResultDto<T>> InsertAsync(T item);
    Task<QueryResultDto<T>> EditAsync(T item);
    Task<QueryResultDto<T>> DeleteAsync(int id);
}