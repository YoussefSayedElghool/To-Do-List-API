using Microsoft.AspNetCore.Identity;
using To_Do_List_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace To_Do_List_API.Models
{
    public class ToDo
    {
        public int ToDoId { get; set; }
        public string Description { get; set; }
        public bool Iscompleted { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }


    }
}