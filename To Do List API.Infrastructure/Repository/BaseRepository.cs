using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;

namespace To_Do_List_API.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        AppDbContext context;
        public BaseRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<QueryResultDto<T>> GetByIdAsync(int id)
        {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null)
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            return new QueryResultDto<T>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<List<T>>> GetWhereAsync(Expression<Func<T, bool>> criteria)
        {
            var result = context.Set<T>().Where(criteria);
            if (result == null)
                return new QueryResultDto<List<T>>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            return new QueryResultDto<List<T>>() { IsCompleteSuccessfully = true, Result = await result.ToListAsync() };
        }

        public async Task<QueryResultDto<List<T>>> GetAllAsync()
        {
            var result = await context.Set<T>().ToListAsync();
            return new QueryResultDto<List<T>>() { IsCompleteSuccessfully = true, Result = result };
        }

        public async Task<QueryResultDto<T>> DeleteAsync(int id)
        {
            if (id == 0)
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };

            QueryResultDto<T> old = await GetByIdAsync(id);

            if (!old.IsCompleteSuccessfully)
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false };

            try
            {
                context.Set<T>().Remove(old.Result);
                await context.SaveChangesAsync();
                return new QueryResultDto<T>() { IsCompleteSuccessfully = true, Result = old.Result };

            }
            catch (Exception ex)
            {
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };
            }

        }

        public async Task<QueryResultDto<T>> EditAsync(T item)
        {

            if (item == null)
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                context.Set<T>().Update(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<T>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected };
            }
        }

        public async Task<QueryResultDto<T>> InsertAsync(T item)
        {
            if (item == null)
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.NotFound };

            try
            {
                await context.Set<T>().AddAsync(item);
                await context.SaveChangesAsync();
                return new QueryResultDto<T>() { IsCompleteSuccessfully = true, Result = item };
            }
            catch (Exception)
            {
                return new QueryResultDto<T>() { IsCompleteSuccessfully = false, Result = item, ErrorMessages = ErrorMessageUserConst.Unexpected };
            }
        }

    }
}
