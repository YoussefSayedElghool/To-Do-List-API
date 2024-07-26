using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Repository.abstraction_layer
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<QueryResultDto<RefreshToken>> GetActiveRefreshTokenAsync(IUserBase user);
        Task<QueryResultDto<IUserBase>> RevokeRefreshTokenAsync(string refreshToken);

    }
}
