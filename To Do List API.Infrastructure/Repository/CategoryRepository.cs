using Microsoft.EntityFrameworkCore;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        AppDbContext context;
        public CategoryRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<QueryResultDto<Category>> GetByIdAsync(int id)
        {
            var toDo = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (toDo == null)
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            return new QueryResultDto<Category>() { IsCompleteSuccessfully = true, Result = toDo };
        }

        public async Task<QueryResultDto<List<Category>>> GetAllAsync()
        {
            var result = await context.Categories.ToListAsync();
            return new QueryResultDto<List<Category>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<Category>> DeleteAsync(int id)
        {
            if (id == 0)
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };

            QueryResultDto<Category> old = await GetByIdAsync(id);

            if (!old.IsCompleteSuccessfully)
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false };

            try
            {
                context.Categories.Remove(old.Result);
                await context.SaveChangesAsync();
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = true, Result = old.Result };

            }
            catch (Exception ex)
            {
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };
            }

        }

        public async Task<QueryResultDto<Category>> EditAsync(Category item)
        {

            if (item == null)
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                context.Categories.Update(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected } ;
            }
        }

        public async Task<QueryResultDto<Category>> InsertAsync(Category item)
        {
            if (item == null)
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                await context.Categories.AddAsync(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<Category>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected } ;
            }
        }

      
    }
}



