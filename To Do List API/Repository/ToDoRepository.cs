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
        public async Task<QueryResultDto<ToDo>> GetById(int id)
        {
            var toDo = await context.ToDos.FirstOrDefaultAsync(x => x.ToDoId == id);
            if (toDo == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.NotFound);

            return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = toDo };
        }

        public async Task<QueryResultDto<List<ToDo>>> GetAllBy(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.NotFound);

            var result = await context.ToDos.Where(x => x.Users.Id == userId).ToListAsync();
            return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<List<ToDo>>> GetAllBy(string userId, int CategoryId)
        {
            if (string.IsNullOrEmpty(userId) || CategoryId == 0)
                return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.IncorrectInput);

            var result = await context.ToDos.Where(x => (x.Users.Id == userId) && (x.CategoryId == CategoryId)).ToListAsync();
            return new QueryResultDto<List<ToDo>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<ToDo>> Delete(int id)
        {
            if (id == 0)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.IncorrectInput);

            QueryResultDto<ToDo> old = await GetById(id);

            if (!old.IsCompleteSuccessfully)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false };

            try
            {
                context.ToDos.Remove(old.Result);
                context.SaveChanges();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = old.Result };

            }
            catch (Exception ex)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.Unexpected); ;
            }

        }

        public QueryResultDto<ToDo> Edit(ToDo item)
        {

            if (item == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.NotFound);

            try
            {
                context.ToDos.Update(item);
                context.SaveChanges();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, Result = item }.AddErrorMessage(ErrorMessageUserConst.Unexpected); ;
            }
        }

        public async Task<QueryResultDto<ToDo>> Insert(ToDo item)
        {
            if (item == null)
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false }.AddErrorMessage(ErrorMessageUserConst.NotFound);

            try
            {
                await context.ToDos.AddAsync(item);
                context.SaveChanges();
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<ToDo>() { IsCompleteSuccessfully = false, Result = item }.AddErrorMessage(ErrorMessageUserConst.Unexpected); ;
            }
        }

      
    }
}


