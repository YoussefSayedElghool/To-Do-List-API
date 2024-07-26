using AutoMapper;
using Microsoft.Extensions.Configuration;
using To_Do_List_API.Core.Repository_Abstraction_Layer;
using To_Do_List_API.Core.Repository_Abstraction_Layer.UnitOfWork;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Infrastructure.Repository.UnitOfWork;
using To_Do_List_API.Models;
using To_Do_List_API.Service.abstraction_layer;
using static To_Do_List_API.Helpers.UploadUtility;

namespace To_Do_List_API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public CategoryService(IRepositoryUnitOfWork repositoryUnitOfWork, IConfiguration configuration, IMapper mapper)
        {
            this.repositoryUnitOfWork = repositoryUnitOfWork;
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


            QueryResultDto<Category> categoryInsertResult = await repositoryUnitOfWork.Categories.InsertAsync(category);
            
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


            QueryResultDto<Category> categoryInsertResult = await repositoryUnitOfWork.Categories.EditAsync(category);

            if (!categoryInsertResult.IsCompleteSuccessfully)
                return new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };


            var categoryResponseDto = mapper.Map<QueryResultDto<CategoryResponseDto>>(categoryInsertResult);

            categoryResponseDto.IsCompleteSuccessfully = true;

            return categoryResponseDto;


        }

        public async Task<QueryResultDto<List<CategoryResponseDto>>> GetAllCategoryAsync()
        {
            var categories = await repositoryUnitOfWork.Categories.GetAllAsync();

            if (!categories.IsCompleteSuccessfully)
                new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };

            var categoryResponseDto = mapper.Map<QueryResultDto<List<CategoryResponseDto>>>(categories);

            categoryResponseDto.IsCompleteSuccessfully = true;
            return categoryResponseDto;
        }

        public async Task<QueryResultDto<CategoryResponseDto>> RemoveCategoryAsync(int id)
        {
            var categories = await repositoryUnitOfWork.Categories.DeleteAsync(id);

            if (!categories.IsCompleteSuccessfully)
                new QueryResultDto<CategoryResponseDto>() { IsCompleteSuccessfully = false, ErrorMessages = ErrorMessageUserConst.Unexpected };

            var categoryResponseDto = mapper.Map<QueryResultDto<CategoryResponseDto>>(categories);

            categoryResponseDto.IsCompleteSuccessfully = true;
            return categoryResponseDto;
        }
    }
}
