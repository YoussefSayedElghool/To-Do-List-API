

using Microsoft.AspNetCore.Identity;

namespace To_Do_List_API.Models
{
    public class User : IdentityUser, IUserBase
    {
        public required string DisplayName { get; set; }
        public string Image { get; set; }

        public virtual List<ToDo> ToDos { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
