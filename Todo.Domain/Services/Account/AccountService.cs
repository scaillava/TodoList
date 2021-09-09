using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Data;
using Todo.Domain.Models;

namespace Todo.Domain.Services.Account
{
    public class AccountService : AccountInterface
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private const int _expirationHours = 5;
        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserToken> Login(string email, string password)
        {
            var applicationUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, false);
            if (result.Succeeded)
            {
                UserToken userToken = await _applicationDbContext.TokenEntity.Where(x => x.AspNetUserId == applicationUser.Id).FirstOrDefaultAsync();
                if (userToken == null)
                {
                    userToken = new UserToken()
                    {
                        AspNetUserId = applicationUser.Id,
                        Expiration = DateTime.Now.AddHours(GetExpirationHours()),
                        Token = Guid.NewGuid()
                    };
                    _applicationDbContext.Add(userToken);
                    await _applicationDbContext.SaveChangesAsync();
                    userToken.AspNetUser = applicationUser;
                }
                else if (userToken.Expiration < DateTime.Now)
                {
                    userToken.Token = Guid.NewGuid();
                    userToken.Expiration = DateTime.Now.AddHours(GetExpirationHours());
                    _applicationDbContext.Update(userToken);
                    await _applicationDbContext.SaveChangesAsync();
                }

                return userToken;

            }
            return null;
        }

        public async Task<IEnumerable<IdentityError>> Register(string name, string email, string password)
        {
            try
            {
                var user = new ApplicationUser { UserName = name, Email = email };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return null;
                }
                return result.Errors;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int GetExpirationHours()
        {
            return _expirationHours;
        }
    }
}
