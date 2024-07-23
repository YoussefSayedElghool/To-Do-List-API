using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using To_Do_List_API.DTO;
using To_Do_List_API.Helpers;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;
using To_Do_List_API.Service;
using To_Do_List_API.Service.abstraction_layer;

namespace To_Do_List_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

       
        [HttpGet]
        public async Task<ActionResult<QueryResultDto<IEnumerable<CategoryResponseDto>>>> GetCategories()
        {

            var result = await categoryService.GetAllCategoryAsync();

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);

            return Ok(result);

        }

       
        [HttpPut]
        public async Task<ActionResult<QueryResultDto<CategoryResponseDto>>> EditCategory([FromForm] CategoryRequestDto categoryRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new QueryResultDto<CategoryResponseDto>()
                {
                    IsCompleteSuccessfully = false,
                    ErrorMessages = ErrorMessageUserConst.Custom(400, string.Join("\n", ModelState.Values.SelectMany(v => v.Errors)))
                });

            var result = await categoryService.EditCategoryAsync(categoryRequestDto);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);

            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult<QueryResultDto<CategoryResponseDto>>> AddCategory([FromForm] CategoryRequestDto categoryRequestDto)
        {
            Console.WriteLine("Test");
            if (!ModelState.IsValid)
                return BadRequest(new QueryResultDto<CategoryResponseDto>()
                {
                    IsCompleteSuccessfully = false,
                    ErrorMessages = ErrorMessageUserConst.Custom(400, string.Join("\n", ModelState.Values.SelectMany(v => v.Errors)))
                });

            var result = await categoryService.AddCategoryAsync(categoryRequestDto);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<QueryResultDto<CategoryResponseDto>>> DeleteCategory(int id)
        {
            var result = await categoryService.RemoveCategoryAsync(id);

            if (!result.IsCompleteSuccessfully)
                return StatusCode(result.ErrorMessages.StatusCode, result);

            return Ok(result);
        }





    }
}
