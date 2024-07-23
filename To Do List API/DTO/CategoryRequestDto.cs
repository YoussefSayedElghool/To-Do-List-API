using System.Text.Json.Serialization;

namespace To_Do_List_API.DTO
{
    public class CategoryRequestDto
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }

        public IFormFile? ImageFile
        {
            get;
            set;
        }
    }
}