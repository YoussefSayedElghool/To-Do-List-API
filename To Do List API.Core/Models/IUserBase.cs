namespace To_Do_List_API.Models
{

    public interface IUserBase
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }

        public List<ToDo> ToDos { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

    }
}

