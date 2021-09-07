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
                    .Where(x => x.Id == id && x.AspNetUsersId == _applicationDbContext.GetUserId(User)).Include(x => x.TodoChecks).FirstOrDefaultAsync();
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
                    .Where(x => x.AspNetUsersId == _applicationDbContext.GetUserId(User) && x.Deleted == null).Include(x => x.TodoChecks).ToListAsync();
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
                if (todo.TodoChecks.Count() > 0 && !todo.TodoChecks.ToList().Exists(x => x.Id == 0))
                {
                    todo.Id = 0;
                    todo.Created = DateTime.Now;
                    todo.Edited = DateTime.Now;
                    todo.Deleted = null;
                    todo.AspNetUsersId = _applicationDbContext.GetUserId(User);
                    _applicationDbContext.Add(todo);
                    await _applicationDbContext.SaveChangesAsync();
                    foreach (var todoCheck in todo.TodoChecks)
                    {
                        todoCheck.TodoId = todo.Id;
                        //todoCheck.Created = DateTime.Now;
                        todoCheck.Edited = DateTime.Now;
                        _applicationDbContext.Add(todoCheck);
                    }
                    await _applicationDbContext.SaveChangesAsync();
                }

                return await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == todo.Id && x.AspNetUsersId == _applicationDbContext.GetUserId(User));
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
                Models.Todo _todo = await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == todo.Id && x.AspNetUsersId == _applicationDbContext.GetUserId(User));
                if (_todo != null)
                {
                    todo.Edited = DateTime.Now;
                    todo.Deleted = null;
                    _applicationDbContext.Update(todo);
                    foreach (var todoCheck in _todo.TodoChecks)
                    {
                        if (!todo.TodoChecks.ToList().Exists(x => x.Id == todo.Id))
                        {
                            _applicationDbContext.Remove(todoCheck);
                        }
                    }
                    foreach (var todoCheck in todo.TodoChecks)
                    {
                        if (todoCheck.Id == 0)
                        {
                            todoCheck.TodoId = todo.Id;
                            todoCheck.Edited = DateTime.Now;
                            _applicationDbContext.Add(todoCheck);
                        }
                        else
                        {
                            todoCheck.Edited = DateTime.Now;
                            _applicationDbContext.Update(todoCheck);
                        }

                    }
                    await _applicationDbContext.SaveChangesAsync();
                    return await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == todo.Id && x.AspNetUsersId == _applicationDbContext.GetUserId(User));
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
                Models.Todo _todo = await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == id && x.AspNetUsersId == _applicationDbContext.GetUserId(User) && x.Deleted == null);
                if (_todo != null)
                {
                    _todo.Edited = DateTime.Now;
                    _todo.Deleted = DateTime.Now;
                    _applicationDbContext.Update(_todo);
                    await _applicationDbContext.SaveChangesAsync();
                    return await _applicationDbContext.TodoEntity.FirstOrDefaultAsync(x => x.Id == id && x.AspNetUsersId == _applicationDbContext.GetUserId(User));
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
