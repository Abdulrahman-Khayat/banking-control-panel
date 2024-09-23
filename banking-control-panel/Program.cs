using System.Reflection;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Models;
using banking_control_panel.Services.ClientServices;
using banking_control_panel.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Remmsh.Extensions;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddProblemDetails();

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("database"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<PasswordHasher<User>>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomJwt(builder.Configuration);

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// app.MapPointRedemptionRoutes();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
