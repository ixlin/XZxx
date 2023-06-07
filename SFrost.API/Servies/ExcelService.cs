using OfficeOpenXml;

public class ExcelService : IExcelService
{
    private readonly AppDbContext _dbContext;

    public ExcelService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ImportExcelData(string filePath)
    {
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0]; // 假设数据在第一个工作表中

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // 假设数据从第二行开始，第一行是标题行
            {
                var moduleName = worksheet.Cells[row, 1].Value?.ToString();
                var taskItem = worksheet.Cells[row, 2].Value?.ToString();
                var startTime = Convert.ToDateTime(worksheet.Cells[row, 3].Value);
                var endTime = Convert.ToDateTime(worksheet.Cells[row, 4].Value);
                var taskOwner = worksheet.Cells[row, 5].Value?.ToString();
                var taskNotes = worksheet.Cells[row, 6].Value?.ToString();

                var excelData = new ExcelData
                {
                    ModuleName = moduleName,
                    TaskItem = taskItem,
                    StartTime = startTime.ToUniversalTime(),
                    EndTime = endTime.ToUniversalTime(),
                    TaskOwner = taskOwner,
                    TaskNotes = taskNotes,
                    ImportTime = DateTime.Now.ToUniversalTime() // 使用 UTC 时间存储
                };

                _dbContext.ExcelData.Add(excelData);
            }

            _dbContext.SaveChanges();
        }
    }
}
