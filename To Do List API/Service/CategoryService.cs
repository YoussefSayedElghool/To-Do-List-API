using AutoMapper;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;
using To_Do_List_API.Service.abstraction_layer;
using static Online_Food_Ordering_System.Models.UploadUtility;

namespace To_Do_List_API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IConfiguration configuration, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<QueryResultDto<CategoryResponseDto>> AddCategoryAsync(CategoryRequestDto item)
        {
            if (item.ImageFile == null)
                return new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };


            UploadOperationResult uploadResult = UploadImage(item.ImageFile, configuration["RootUploadImagePath"], configuration["SuperFolderUploadImage"]);

            if (!uploadResult.IsSuccess) new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.ImageUploadErorr };


            var category = mapper.Map<Category>(item);
            category.BacbackgroundImage = uploadResult.RelastivePath;


            QueryResultDto<Category> categoryInsertResult = await categoryRepository.InsertAsync(category);
            
            if (!categoryInsertResult.IsCompleteSuccessfully)
                new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };


            var categoryResponseDto = mapper.Map<QueryResultDto<CategoryResponseDto>>(categoryInsertResult);

            categoryResponseDto.IsCompleteSuccessfully = true;

            return categoryResponseDto;

        }

        public async Task<QueryResultDto<CategoryResponseDto>> EditCategoryAsync(CategoryRequestDto item)
        {
            if (item.ImageFile == null)
                return new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.IncorrectInput };


            UploadOperationResult uploadResult = UploadImage(item.ImageFile, configuration["RootUploadImagePath"], configuration["SuperFolderUploadImage"]);

            if (!uploadResult.IsSuccess) return new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.ImageUploadErorr };


            var category = mapper.Map<Category>(item);
            category.BacbackgroundImage = uploadResult.RelastivePath;


            QueryResultDto<Category> categoryInsertResult = await categoryRepository.EditAsync(category);

            if (!categoryInsertResult.IsCompleteSuccessfully)
                return new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };


            var categoryResponseDto = mapper.Map<QueryResultDto<CategoryResponseDto>>(categoryInsertResult);

            categoryResponseDto.IsCompleteSuccessfully = true;

            return categoryResponseDto;


        }

        public async Task<QueryResultDto<List<CategoryResponseDto>>> GetAllCategoryAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            if (!categories.IsCompleteSuccessfully)
                new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };

            var categoryResponseDto = mapper.Map<QueryResultDto<List<CategoryResponseDto>>>(categories);

            categoryResponseDto.IsCompleteSuccessfully = true;
            return categoryResponseDto;
        }

        public async Task<QueryResultDto<CategoryResponseDto>> RemoveCategoryAsync(int id)
        {
            var categories = await categoryRepository.DeleteAsync(id);

            if (!categories.IsCompleteSuccessfully)
                new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };

            var categoryResponseDto = mapper.Map<QueryResultDto<CategoryResponseDto>>(categories);

            categoryResponseDto.IsCompleteSuccessfully = true;
            return categoryResponseDto;
        }
    }
}
