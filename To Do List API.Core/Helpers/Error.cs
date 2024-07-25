namespace To_Do_List_API.Helpers
{
    public class Error {
        public Error(ushort statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public ushort StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }


        public override string ToString()
        {
            return ErrorMessage;
        }

    }
}
