using CoreAPI.Core.Configuration;
using CoreAPI.Core.Models;
using CoreAPI.Core.Repositories;
using CoreAPI.Core.Services;
using CoreAPI.Core.UnitOfWork;
using CoreAPI.Data;
using CoreAPI.Data.Repositories;
using CoreAPI.Service.Mapper;
using CoreAPI.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Libary.Configuration;
using Shared.Libary.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI Register
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//db context
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


//options patterns: to reach appsetting from di
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));


//configure jwt
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();
builder.Services.AddCustomTokenAuth(tokenOptions);

//add mapper
builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
