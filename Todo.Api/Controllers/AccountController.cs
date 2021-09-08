using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;
using Todo.Domain;
using Todo.Domain.Services.Account;
using Todo.Domain.Services.Todo;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly AccountInterface _accountInterface;
        private readonly IMapper _mapper;
        public AccountController(ILogger<AccountController> logger, AccountInterface accountInterface, IMapper mapper)
        {
            _logger = logger;
            _accountInterface = accountInterface;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountInterface.Login(loginViewModel.Email, loginViewModel.Password);
                if (result != null)
                {
                    return Ok(_mapper.Map<TokenViewModel>(result));
                }
            }
            return BadRequest();
        }

        // POST: /Account/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountInterface.Register(registerViewModel.Name, registerViewModel.Email, registerViewModel.Password);
                if (result != null)
                {
                    return BadRequest(result);
                }
                return NoContent();

            }
            return BadRequest();
        }
    }
}
