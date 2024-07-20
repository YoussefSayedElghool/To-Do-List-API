using Microsoft.AspNetCore.Identity;
using To_Do_List_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace To_Do_List_API.Models
{
    public class User :IdentityUser
    {
        public required string DisplayName { get; set; }
        public string Image { get; set; }

        public virtual List<ToDo> ToDos { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
