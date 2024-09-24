using System.Reflection;
using banking_control_panel.Data;
using banking_control_panel.Data.ClientRepo;
using banking_control_panel.Data.SearchRepo;
using banking_control_panel.Models;
using banking_control_panel.Services.ClientServices;
using banking_control_panel.Services.SearchService;
using banking_control_panel.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Remmsh.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console() 
    .CreateLogger();

// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("database"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientRepo, ClientRepo>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISearchRepo, SearchRepo>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<PasswordHasher<User>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddCustomJwt(builder.Configuration);
builder.Host.UseSerilog();
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
