using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Todo.Domain.Services.Todo
{
    public class TodoListService : TodoListInterface
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TodoListService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Models.Todo> GetTodoList(int id, System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                return await _applicationDbContext.TodoEntity
                    .Where(x => x.Id == id && x.AspNetUserId == _applicationDbContext.GetUserId(User)).Include(x => x.TodoTasks).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Models.Todo>> GetTodoLists(System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                return await _applicationDbContext.TodoEntity
                    .Where(x => x.AspNetUserId == _applicationDbContext.GetUserId(User) && x.Deleted == null).Include(x => x.TodoTasks).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Todo> CreateTodoList(Models.Todo todo, System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                todo.Id = 0;
                todo.Created = DateTime.Now;
                todo.Edited = DateTime.Now;
                todo.Deleted = null;
                todo.AspNetUserId = _applicationDbContext.GetUserId(User);
                foreach (var todoTask in todo.TodoTasks)
                {
                    todoTask.Id = 0;
                    todoTask.Edited = DateTime.Now;
                }
                _applicationDbContext.Add(todo);
                await _applicationDbContext.SaveChangesAsync();
                return todo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Models.Todo> UpdateTodoList(Models.Todo todo, System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                Models.Todo _todo = await _applicationDbContext.TodoEntity.Where(x => x.Id == todo.Id && x.AspNetUserId == _applicationDbContext.GetUserId(User)).Include(x=>x.TodoTasks).FirstOrDefaultAsync();
                if (_todo != null)
                {
                    _todo.Edited = DateTime.Now;
                    _todo.Deleted = null;
                    _todo.Title = todo.Title;
                    _todo.Position = todo.Position;
                    _applicationDbContext.TodoEntity.Update(_todo);
                    foreach (var _todoTask in _todo.TodoTasks)
                    {
                        var todoTask = todo.TodoTasks.ToList().FirstOrDefault((x => x.Id == _todoTask.Id));
                        if (todoTask == null)
                        {
                            _applicationDbContext.Remove(_todoTask);
                        }
                        else
                        {
                            _todoTask.Edited = DateTime.Now;
                            _todoTask.Position = todoTask.Position;
                            _todoTask.TaskDescription = todoTask.TaskDescription;
                            _todoTask.Done = todoTask.Done;
                            _applicationDbContext.Update(_todoTask);
                        }
                    }
                    foreach (var todoTask in todo.TodoTasks)
                    {
                        if (todoTask.Id == 0)
                        {
                            todoTask.TodoId = _todo.Id;
                            todoTask.Edited = DateTime.Now;
                            _applicationDbContext.Add(todoTask);
                        }

                    }
                    await _applicationDbContext.SaveChangesAsync();
                    return _todo;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Models.Todo> DeleteTodoList(int id, System.Security.Claims.ClaimsPrincipal User)
        {
            try
            {
                Models.Todo _todo = await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == id && x.AspNetUserId == _applicationDbContext.GetUserId(User) && x.Deleted == null);
                if (_todo != null)
                {
                    _todo.Edited = DateTime.Now;
                    _todo.Deleted = DateTime.Now;
                    _applicationDbContext.Update(_todo);
                    await _applicationDbContext.SaveChangesAsync();
                    return _todo;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
