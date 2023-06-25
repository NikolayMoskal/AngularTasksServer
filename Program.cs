using MediaItemsServer.Helpers;
using MediaItemsServer.Interfaces;
using MediaItemsServer.Middleware;
using MediaItemsServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvcCore()
    .AddApiExplorer()
    .AddCors()
    .AddDataAnnotations()
    .AddFormatterMappings();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddSingleton<IMediaItemsService, MediaItemsService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHeaders();
app.UseAuthentication(new List<string> { Consts.AdministratorRole });

app.MapControllers();

app.Run();
