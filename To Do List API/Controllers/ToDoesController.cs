using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using To_Do_List_API.DTO;
using To_Do_List_API.Models;
using To_Do_List_API.Service.abstraction_layer;

namespace To_Do_List_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesController : ControllerBase
    {
        private readonly IToDoService toDoService;

        public ToDoesController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        // GET: api/ToDoes
        [HttpGet]
        public async Task<ActionResult<QueryResultDto<IEnumerable<ToDoDto>>>> GetToDos(int CategoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QueryResultDto<List<ToDoDto>> queryResultDto = await toDoService.GetToDoesAsync(userId , CategoryId);

            if (!queryResultDto.IsCompleteSuccessfully)
                return StatusCode(queryResultDto.ErrorMessages.StatusCode, queryResultDto);
            
            return Ok(queryResultDto);

        }

        // GET: api/ToDoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoDto>> GetToDo(int id)
        {
            QueryResultDto<List<ToDoDto>> queryResultDto = await toDoService.GetToDoAsync(id);

            if (!queryResultDto.IsCompleteSuccessfully)
                return StatusCode(queryResultDto.ErrorMessages.StatusCode, queryResultDto);

            return Ok(queryResultDto);
        }

        // PUT: api/ToDoes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoDto>> EditToDo(ToDoDto toDoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new QueryResultDto<ToDoDto>() { Result = toDoDto, IsCompleteSuccessfully = false });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (toDoDto == null || string.IsNullOrEmpty(userId))
                return NotFound(toDoDto);

            QueryResultDto<ToDoDto> queryResultDto = await toDoService.EditToDoAsync(toDoDto , userId);

            if (!queryResultDto.IsCompleteSuccessfully)
                return StatusCode(queryResultDto.ErrorMessages.StatusCode, queryResultDto);

            return Ok(queryResultDto);
            
        }

        // POST: api/ToDoes
        [HttpPost]
        public async Task<ActionResult<ToDoDto>> AddToDo(ToDoDto toDoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new QueryResultDto<ToDoDto>() { Result = toDoDto, IsCompleteSuccessfully = false });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (toDoDto == null || string.IsNullOrEmpty(userId))
                return NotFound(toDoDto);

            QueryResultDto<ToDoDto> queryResultDto = await toDoService.AddToDoAsync(toDoDto, userId);

            if (!queryResultDto.IsCompleteSuccessfully)
                return StatusCode(queryResultDto.ErrorMessages.StatusCode, queryResultDto);

            return Ok(queryResultDto);
        }

        // DELETE: api/ToDoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoDto>> DeleteToDo(int id)
        {
            QueryResultDto<ToDoDto> queryResultDto = await toDoService.RemoveToDoAsync(id);

            if (!queryResultDto.IsCompleteSuccessfully)
                return StatusCode(queryResultDto.ErrorMessages.StatusCode, queryResultDto);

            return Ok(queryResultDto);
        }

       
    }
}
