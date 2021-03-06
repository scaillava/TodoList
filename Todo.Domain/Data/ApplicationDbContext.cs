using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;

namespace Todo.Domain.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Models.Todo>()
            .HasIndex(p => new { p.Id, p.AspNetUserId });
            builder.Entity<Models.TodoTask>()
            .HasIndex(p => new { p.Id, p.TodoId });

        }
        public DbSet<Models.Todo> TodoEntity { get; set; }
        public DbSet<Models.TodoTask> TodoTaskEntity { get; set; }
        public DbSet<Models.UserToken> TokenEntity { get; set; }


        public string GetUserId(System.Security.Claims.ClaimsPrincipal User)
        {
            return User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
