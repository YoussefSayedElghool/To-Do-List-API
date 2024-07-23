namespace To_Do_List_API.Helpers
{
    public static class ErrorMessageUserConst
    {
        public static Error NotFound = new Error(404,"this resource not found");
        public static Error IncorrectInput = new Error(400, "incorrect or Empty input");
        public static Error EmailAlreadyRegistered = new Error(400, "Email is already registered!");
        public static Error incorrectEmailOrPassword = new Error(406, "Email or Password is incorrect!");
        public static Error InvalidToken = new Error(498, "Invalid token");
        public static Error Unexpected = new Error(500, "Unexpected Error");
        public static Error ImageUploadErorr = new Error(500, "Erorr happend when image upload");

        public static Error Custom(ushort code, string error) => new Error(code, error);

    }
}
