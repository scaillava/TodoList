using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Todo.Domain.Services.Todo
{
    public interface TodoListInterface
    {
        Task<Models.Todo> CreateTodoList(Models.Todo todo, ClaimsPrincipal User);
        Task<Models.Todo> DeleteTodoList(int id, ClaimsPrincipal User);
        Task<Models.Todo> GetTodoList(int id, ClaimsPrincipal User);
        Task<List<Models.Todo>> GetTodoLists(ClaimsPrincipal User);
        Task<Models.Todo> UpdateTodoList(Models.Todo todo, ClaimsPrincipal User);
    }
}