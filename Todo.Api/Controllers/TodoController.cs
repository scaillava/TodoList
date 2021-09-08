using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;
using Todo.Domain.Services.Todo;

namespace Todo.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : Controller
    {
        private readonly TodoListInterface _todoInterface;
        private readonly ILogger<TodoController> _logger;
        private readonly IMapper _mapper;

        public TodoController(ILogger<TodoController> logger, TodoListInterface todoInterface, IMapper mapper)
        {
            _logger = logger;
            _todoInterface = todoInterface;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(List<Domain.Models.Todo>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetTodoLists()
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
        public async Task<IActionResult> GetTodoList([FromRoute] int id)
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

        [ProducesResponseType(typeof(Domain.Models.Todo), StatusCodes.Status200OK)]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteTodoList([FromRoute] int id)
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


        [ProducesResponseType(typeof(Domain.Models.Todo), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> PostTodoList([FromBody] UpsertTodoViewModel upsertTodoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _todoInterface.CreateTodoList(_mapper.Map<Domain.Models.Todo>(upsertTodoViewModel), User);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ProducesResponseType(typeof(Domain.Models.Todo), StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> PutTodoList([FromBody] UpsertTodoViewModel upsertTodoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _todoInterface.UpdateTodoList(_mapper.Map<Domain.Models.Todo>(upsertTodoViewModel), User);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
