using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.Commercial;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IExcelDataService,ExcelDataService>();

// 添加 SFrost.API 项目的数据库上下文
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//注册：数据库上下文（ApplicationDbContext这个类是可以自定义命名的）、使用指定连接字符串
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Enable directory browsing for static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "XLSXS")),
    RequestPath = "/files"
});

// Enable serving of the index page when accessing the root URL
app.UseDefaultFiles();

// Enable directory browsing when accessing the root URL
app.UseDirectoryBrowser();

app.Run();
