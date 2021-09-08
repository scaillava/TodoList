using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Data;
using Todo.Domain.Services.Account;
using Todo.Domain.Services.Todo;

namespace Todo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme
                    = nameof(ValidateCustomAuthenticationHandler);
            })
            .AddScheme<ValidateCustomAuthenticationSchemeOptions, ValidateCustomAuthenticationHandler>
                    (nameof(ValidateCustomAuthenticationHandler), op => { });
            services.AddAuthorization(o =>
            {
                var builder = new AuthorizationPolicyBuilder(nameof(ValidateCustomAuthenticationHandler));
                builder = builder.RequireClaim(ClaimTypes.Email);
                o.DefaultPolicy = builder.Build();
            });

            services.AddTransient<TodoListInterface, TodoListService>();
            services.AddTransient<AccountInterface, AccountService>();
            services.AddSwaggerGen(c =>
            {

                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TodoList API",
                    Description = "TodoList API (ASP.NET Core 3.1)",
                });
                c.ResolveConflictingActions(x => x.First());

                // Add Authorization onto swagger
                c.AddSecurityDefinition("access_token", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "Guid",
                    In = ParameterLocation.Header,
                    Description = "Acess token",
                });



                // Add Operation Specific Authorization - this will add lock to endpoints requiring authorization
                c.OperationFilter<AuthOperationFilter>();



                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
