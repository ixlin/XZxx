using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ExcelController : ControllerBase
{
    private readonly IExcelService _excelService;

    public ExcelController(IExcelService excelService)
    {
        _excelService = excelService;
    }

    [HttpPost("import")]
    public IActionResult ImportExcelData()
    {
        try
        {
            var xlsPath = Path.Combine(AppContext.BaseDirectory, "xls", "20230604232433.xlsx");
            var fileStream = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);

            // 读取 Excel 文件的逻辑...

            _excelService.ImportExcelData(xlsPath); // 调用 ExcelService 的方法来读取并存储 Excel 数据

            return Ok("Excel data imported successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

}
