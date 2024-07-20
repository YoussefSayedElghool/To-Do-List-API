using Microsoft.AspNetCore.Mvc;
using To_Do_List_API.DTO;
using To_Do_List_API.Service.abstraction_layer;

namespace To_Do_List_API.Controllers
{
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountingService accountingService;

        public AccountsController(IAccountingService accountingService)
        {
            this.accountingService = accountingService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<QueryResultDto<AccountDto>>> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto is null || !ModelState.IsValid)
                return BadRequest(ModelState); // hint : 

            var result = await accountingService.RegisterAsync(registerDto);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);


            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<QueryResultDto<AccountDto>>> LoginAsync(LoginDto loginDto)
        {

            if (loginDto is null || !ModelState.IsValid)
                return BadRequest(ModelState); // hint : 

            var result = await accountingService.LoginAsync(loginDto);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);


            return Ok(result);
        }

        [HttpGet("refreshToken")]
        public async Task<ActionResult<QueryResultDto<AccountDto>>> RefreshToken(RevokeTokenDto revokeTokenDto)
        {
            var refreshToken = revokeTokenDto.refreshToken;

            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshToken))
                return BadRequest(ModelState); // hint


            var result = await accountingService.RefreshTokenAsync(refreshToken);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);


            return Ok(result);
        }

        [HttpDelete("revokeToken")]
        public async Task<ActionResult<QueryResultDto<bool>>> RevokeToken([FromBody] RevokeTokenDto revokeTokenDto)
        {

            var refreshToken = revokeTokenDto.refreshToken;
            if (!ModelState.IsValid || string.IsNullOrEmpty(refreshToken))
                return BadRequest(ModelState); // hint


            var result = await accountingService.RevokeTokenAsync(refreshToken);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);

            return Ok(result);
        }



    }
}
