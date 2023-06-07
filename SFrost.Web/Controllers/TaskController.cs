using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Linq;

namespace SFrost.API.Controllers
{
    public class TaskController : Controller
    {
        private readonly IExcelDataService _excelDataService;
        private readonly IWebHostEnvironment _environment;

        public TaskController(IExcelDataService excelDataService, IWebHostEnvironment environment)
        {
            _excelDataService = excelDataService;
            _environment = environment;
        }


        public IActionResult Index(int page = 1)
        {
            var excelDataList = _excelDataService.GetExcelDataList();

            return View(excelDataList);
        }

        [HttpPost]
        public IActionResult Delete(int[] selectedIds)
        {
            _excelDataService.DeleteExcelData(selectedIds);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult ProcessAction(string action, int[] selectedIds)
        {
            if (action == "delete")
            {
                // 执行删除操作，根据selectedIds删除相应的数据
                // ...
                _excelDataService.DeleteExcelData(selectedIds);
                return RedirectToAction("Index");
            }
            else if (action == "export")
            {
                // 执行导出操作，根据selectedIds导出相应的数据
                // ...
                // 获取选择的记录
                var selectedData = _excelDataService.GetSelectedExcelData(selectedIds.ToList<int>());

                // 加载Excel模板文件
                var targetDirectory = "output"; // 设置目标文件保存目录
                string fileName = "ExcelTemplate.xlsx";

                var templateFilePath = Path.Combine(_environment.WebRootPath, targetDirectory, fileName);
                var templateFile = new FileInfo(templateFilePath);
                using (var package = new ExcelPackage(templateFile))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // 假设数据在第一个工作表中
                    int row = 2; // 从第二行开始写入数据

                    // 将选择的记录填充到Excel模板中
                    foreach (var data in selectedData)
                    {
                        worksheet.Cells[row, 1].Value = data.ModuleName;
                        worksheet.Cells[row, 2].Value = data.TaskItem;
                        worksheet.Cells[row, 3].Value = data.StartTime;
                        worksheet.Cells[row, 3].Style.Numberformat.Format = "yyyy-MM-dd"; // 设置日期格式
                        worksheet.Cells[row, 4].Value = data.EndTime;
                        worksheet.Cells[row, 4].Style.Numberformat.Format = "yyyy-MM-dd"; // 设置日期格式
                        worksheet.Cells[row, 5].Value = data.TaskOwner;
                        worksheet.Cells[row, 6].Value = data.TaskNotes;
                        row++;
                    }

                    // 生成导出的Excel文件
                    var result = new MemoryStream(package.GetAsByteArray());

                    // 生成文件名，包含当前时间
                    string exportFileName = "Export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                    // 设置响应头，将文件下载到客户端
                    Response.Headers.Add("Content-Disposition", "attachment; filename=" + exportFileName);
                    Response.Headers.Add("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    // 返回导出的Excel文件流
                    return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", exportFileName);
                }
            }
            return Ok("不一定成功");
            // 返回视图或重定向到其他页面
        }
        
        public void ExportToExcel(List<int> selectedIds)
        {
            
        }

        
    }
}
