using Microsoft.AspNetCore.Identity;
using To_Do_List_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List_API.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsRevoked => RevokedOn != null;
        public bool IsActive => RevokedOn == null && !IsExpired;

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
