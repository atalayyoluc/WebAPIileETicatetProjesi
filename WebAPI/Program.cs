using Busines.Abstract;
using Busines.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IUserDal,EfUserDal>();
builder.Services.AddTransient<IUserService,UserService>();
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=.;Database=WebAPIETicaretDB;Trusted_Connection=True;",option=>option.MigrationsAssembly("DataAccess").MigrationsHistoryTable(HistoryRepository.DefaultTableName,"dbo")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
