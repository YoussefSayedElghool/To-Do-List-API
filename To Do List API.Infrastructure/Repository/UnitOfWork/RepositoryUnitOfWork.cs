using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.Core.Repository_Abstraction_Layer.UnitOfWork;
using To_Do_List_API.Models;
using To_Do_List_API.Repository;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Infrastructure.Repository.UnitOfWork
{
    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        private readonly AppDbContext _context;

        public RepositoryUnitOfWork(AppDbContext context, IToDoRepository toDoes, ICategoryRepository categories, IRefreshTokenRepository refreshTokens)
        {
            _context = context;
            ToDoes = toDoes;
            Categories = categories;
            RefreshTokens = refreshTokens;
        }

        public IToDoRepository ToDoes { get; private set; }
         
        public ICategoryRepository Categories { get; private set; }

        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
