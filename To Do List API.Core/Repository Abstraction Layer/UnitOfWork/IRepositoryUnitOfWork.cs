using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Repository.abstraction_layer;

namespace To_Do_List_API.Core.Repository_Abstraction_Layer.UnitOfWork
{
    public interface IRepositoryUnitOfWork : IDisposable
    {
        IToDoRepository ToDoes { get; }
        ICategoryRepository Categories { get; }
        IRefreshTokenRepository RefreshTokens { get; }
    }
}
