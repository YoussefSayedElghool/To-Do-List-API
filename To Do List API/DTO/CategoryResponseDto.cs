using System.Text.Json.Serialization;

namespace To_Do_List_API.DTO
{
    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string BacbackgroundImage { get; set; }

     }
}
