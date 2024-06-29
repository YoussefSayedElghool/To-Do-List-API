using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List_API.DTO
{
    public class ToDoDto
    {
        public int ToDoId { get; set; }
        public string Description { get; set; }
        public bool Iscompleted { get; set; }
        public int CategoryId { get; set; }
    }
}
