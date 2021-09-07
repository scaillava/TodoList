using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Services.Todo;

namespace Todo.App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : Controller
    {
        private readonly TodoListInterface _todoInterface;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger, TodoListInterface todoInterface)
        {
            _logger = logger;
            _todoInterface = todoInterface;
        }

        [ProducesResponseType(typeof(List<Domain.Models.Todo>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetTodo()
        {
            try
            {
                var result = await _todoInterface.GetTodoLists(User);
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [ProducesResponseType(typeof(Domain.Models.Todo), StatusCodes.Status200OK)]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodoById([FromRoute] int id)
        {
            try
            {
                var result = await _todoInterface.GetTodoList(id, User);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ProducesResponseType(typeof(List<Domain.Models.Todo>), StatusCodes.Status200OK)]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            try
            {
                var result = await _todoInterface.DeleteTodoList(id, User);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
