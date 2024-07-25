using To_Do_List_API.DTO;

namespace To_Do_List_API.Service.abstraction_layer
{
    public interface IAccountingService
    {
        Task<QueryResultDto<AccountDto>> RegisterAsync(RegisterDto registerDto);
        Task<QueryResultDto<AccountDto>> LoginAsync(LoginDto model);
        Task<QueryResultDto<AccountDto>> RefreshTokenAsync(string token);
        Task<QueryResultDto<bool>> RevokeTokenAsync(string token);
    }
}
