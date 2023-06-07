using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.Commercial;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IExcelDataService, ExcelDataService>();
builder.Services.AddSingleton<IHostEnvironment>(builder.Environment);

//获取数据库连接信息
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//注册：数据库上下文（ApplicationDbContext这个类是可以自定义命名的）、使用指定连接字符串
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

