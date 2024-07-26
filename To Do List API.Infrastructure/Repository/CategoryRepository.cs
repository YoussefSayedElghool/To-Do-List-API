using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Infrastructure.Repository;
using To_Do_List_API.Models;


namespace To_Do_List_API.Repository
{
    public class CategoryRepository : BaseRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext _context) : base(_context)
        {
        }

    }
}



