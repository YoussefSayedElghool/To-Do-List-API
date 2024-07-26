using AutoMapper;
using To_Do_List_API.Core.Repository_Abstraction_Layer.UnitOfWork;
using To_Do_List_API.DTO;
using To_Do_List_API.Infrastructure.Repository.UnitOfWork;
using To_Do_List_API.Models;
using To_Do_List_API.Repository.abstraction_layer;
using To_Do_List_API.Service.abstraction_layer;

namespace To_Do_List_API.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IRepositoryUnitOfWork repositoryUnitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IRepositoryUnitOfWork repositoryUnitOfWork, IMapper mapper)
        {
            this.repositoryUnitOfWork = repositoryUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<QueryResultDto<List<ToDoDto>>> GetToDoesAsync(string userId, int CategoryId)
        {
            return mapper.Map<QueryResultDto<List<ToDoDto>>>(await repositoryUnitOfWork.ToDoes.GetAllByAsync(userId, CategoryId));
        }
        
        public async Task<QueryResultDto<List<ToDoDto>>> GetToDoAsync(int toDoId)
        {
            return mapper.Map<QueryResultDto<List<ToDoDto>>>(await repositoryUnitOfWork.ToDoes.GetByIdAsync(toDoId));
        }

        public async Task<QueryResultDto<ToDoDto>> AddToDoAsync(ToDoDto item, string userId)
        {
            var toDo = mapper.Map<ToDo>(item);
            toDo.UserId = userId;
            var result = await repositoryUnitOfWork.ToDoes.InsertAsync(toDo);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }

        public async Task<QueryResultDto<ToDoDto>> EditToDoAsync(ToDoDto item , string userId)
        {
            var toDo = mapper.Map<ToDo>(item);
            toDo.UserId = userId;

            var result = await repositoryUnitOfWork.ToDoes.EditAsync(toDo);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }

        public async Task<QueryResultDto<ToDoDto>> RemoveToDoAsync(int id)
        {
            var result = await repositoryUnitOfWork.ToDoes.DeleteAsync(id);

            return mapper.Map<QueryResultDto<ToDoDto>>(result);
        }
    }
}
