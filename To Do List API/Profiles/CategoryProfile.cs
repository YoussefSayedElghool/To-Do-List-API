using AutoMapper;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;

namespace To_Do_List_API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequestDto, Category>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>().ReverseMap();
            CreateMap<Category, QueryResultDto<CategoryResponseDto>>().ReverseMap();
            CreateMap<QueryResultDto<List<Category>>, QueryResultDto<List<CategoryResponseDto>>>().ReverseMap();
            CreateMap<QueryResultDto<CategoryResponseDto>, QueryResultDto<Category>>().ReverseMap();
            


        }
    }
}
