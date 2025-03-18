using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.Mappings;
using EmployeeManagementSystem.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployeeDataDbContext>(options =>
{
    options.UseMySQL(builder.Configuration
        .GetConnectionString("EmployeeDBConnectionString")).LogTo(Console.WriteLine, LogLevel.Information); ;
});

builder.Services.AddScoped<IRepository,EmployeeRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
