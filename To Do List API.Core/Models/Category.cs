namespace To_Do_List_API.Models
{
    public class Category{
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string BacbackgroundImage { get; set; }
        public virtual List<ToDo> ToDos { get; set; }
    }
}
