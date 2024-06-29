namespace To_Do_List_API.Helpers
{
    public static class ErrorMessageUserConst
    {
        public static Error NotFound = new Error(404,"this resource not found");
        public static Error IncorrectInput = new Error(400, "incorrect or Empty input");
        public static Error Unexpected = new Error(500, "Unexpected Error");
    }
}
