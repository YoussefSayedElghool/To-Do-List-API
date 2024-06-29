using System.Numerics;
using To_Do_List_API.Helpers;

namespace To_Do_List_API.DTO
{
    public class QueryResultDto<T>
    {
        public bool IsCompleteSuccessfully { get; set; }
        public T Result { get; set; }
        public Error ErrorMessages { get; set; }
    }
}
