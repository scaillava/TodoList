using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Models;

namespace Todo.Domain.Services.Account
{
    public interface AccountInterface
    {
        int GetExpirationHours();
        Task<UserToken> Login(string email, string password);
        Task<IEnumerable<IdentityError>> Register(string name, string email, string password);
    }
}