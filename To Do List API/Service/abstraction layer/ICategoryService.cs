using To_Do_List_API.DTO;

namespace To_Do_List_API.Service.abstraction_layer
{
    public interface ICategoryService
    {
        Task<QueryResultDto<List<CategoryResponseDto>>> GetAllCategoryAsync();
        Task<QueryResultDto<CategoryResponseDto>> AddCategoryAsync(CategoryRequestDto item);
        Task<QueryResultDto<CategoryResponseDto>> EditCategoryAsync(CategoryRequestDto item);
        Task<QueryResultDto<CategoryResponseDto>> RemoveCategoryAsync(int id);
    }
}
