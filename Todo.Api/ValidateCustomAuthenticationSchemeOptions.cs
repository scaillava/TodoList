using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Todo.Domain.Data;
using Todo.Domain.Models;

namespace Todo.Api
{


    public class ValidateCustomAuthenticationSchemeOptions
         : AuthenticationSchemeOptions
    { }

    public class ValidateCustomAuthenticationHandler
        : AuthenticationHandler<ValidateCustomAuthenticationSchemeOptions>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ValidateCustomAuthenticationHandler(
            IOptionsMonitor<ValidateCustomAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ApplicationDbContext applicationDbContext,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _applicationDbContext = applicationDbContext;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            // validation comes in here
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
            }

            var token = Request.Headers["Authorization"].ToString();
            UserToken userToken = _applicationDbContext.TokenEntity.Where(x => x.Token == Guid.Parse(token)).Include(x => x.AspNetUser).FirstOrDefault();
            if (userToken != null && (DateTime.Now < userToken.Expiration))
            {

                // create claims array from the model
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, userToken.AspNetUser.Id.ToString()),
                    new Claim(ClaimTypes.Email, userToken.AspNetUser.Email),
                    new Claim(ClaimTypes.Name, userToken.AspNetUser.UserName) };

                // generate claimsIdentity on the name of the class
                var claimsIdentity = new ClaimsIdentity(claims,
                            nameof(ValidateCustomAuthenticationHandler));

                // generate AuthenticationTicket from the Identity
                // and current authentication scheme
                var ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                // pass on the ticket to the middleware
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail("Token Expired"));
            }
        }
    }

}
