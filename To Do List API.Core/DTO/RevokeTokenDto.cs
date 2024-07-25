
using System.ComponentModel.DataAnnotations;

namespace To_Do_List_API.DTO
{
    public class RevokeTokenDto
    {
            [Required]
            public string refreshToken { get; set; }
        
    }
}
