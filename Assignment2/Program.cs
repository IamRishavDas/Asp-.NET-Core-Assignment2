using Assignment2.Data;
using Assignment2.Helper;
using Assignment2.Repositories;
using Assignment2.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProfileMapper));

builder.Services.TryAddScoped<IEmployeeRepositoryInterface, EmployeeRepository>();
builder.Services.TryAddScoped<IDepartmentRepositoryInterface, DepartmentRepository>();

builder.Services.TryAddScoped<IEmployeeServiceInterface, EmployeeService>();
builder.Services.TryAddScoped<IDepartmentServiceInterface, DepartmentService>();

builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

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
