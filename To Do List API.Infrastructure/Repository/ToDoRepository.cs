using Microsoft.EntityFrameworkCore;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Infrastructure.Repository;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Repository
{
    public class ToDoRepository : BaseRepository<ToDo>, IToDoRepository
    {

        AppDbContext context;
        public ToDoRepository(AppDbContext _context) : base(_context)
        {
            context = _context;
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
      
    }
}



