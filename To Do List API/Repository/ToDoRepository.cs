using Microsoft.EntityFrameworkCore;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Repository
{
    public class ToDoRepository : IToDoRepository
    {

        AppDbContext context;
        public ToDoRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<QueryResultDto<ToDo>> GetByIdAsync(int id)
        {
            var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.ToDoId == id);
            if (toDo == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = toDo };
        }

        public async Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            var result = await context.ToDos.Where(x => x.UserId == userId).ToListAsync();
            return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<List<ToDo>>> GetAllByAsync(string userId, int CategoryId)
        {
            if (string.IsNullOrEmpty(userId) || CategoryId == 0)
                return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };

            var result = await context.ToDos.Where(x => (x.UserId == userId) && (x.CategoryId == CategoryId)).ToListAsync();
            return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<ToDo>> DeleteAsync(int id)
        {
            if (id == 0)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };

            QueryResultDto<ToDo> old = await GetByIdAsync(id);

            if (!old.IsCompleteSuccessfully)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false };

            try
            {
                context.ToDos.Remove(old.Result);
                await context.SaveChangesAsync();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = old.Result };

            }
            catch (Exception ex)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };
            }

        }

        public async Task<QueryResultDto<ToDo>> EditAsync(ToDo item)
        {

            if (item == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                context.ToDos.Update(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected } ;
            }
        }

        public async Task<QueryResultDto<ToDo>> InsertAsync(ToDo item)
        {
            if (item == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                await context.ToDos.AddAsync(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected } ;
            }
        }

      
    }
}



