using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SFrost.Web.Models;

namespace SFrost.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _environment;
    private readonly IExcelService _excelService;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment,
        IExcelService excelService)
    {
        _logger = logger;
        _environment = environment;
        _excelService = excelService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded");
        }

        var targetDirectory = "XLSXS"; // 设置目标文件保存目录

        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string extension = Path.GetExtension(file.FileName);
        string fileName = timestamp + extension;

        var filePath = Path.Combine(_environment.WebRootPath, targetDirectory, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        string msg = "文件：" + file.FileName + "上传成功。<br />";

        // 调用之前实现的 API 接口来读取并保存 Excel 数据
        _excelService.ImportExcelData(filePath);
        msg = msg + "读取文件：" + filePath + "并导入数据成功。";
        return Ok(msg);

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

