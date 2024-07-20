using System.ComponentModel.DataAnnotations;

namespace To_Do_List_API.DTO
{
    public class LoginDto
    {
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format")]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*[^a-zA-Z\d]).{6,}$", ErrorMessage = "the password must consist of 6 digit container Number Upper and Lower character and at least one non-alphanumeric")]
        [Required]
        public string Password { get; set; }
    }  
}