using System.Collections.Generic;



public class ExcelDataService : IExcelDataService
{
    private readonly AppDbContext _dbContext;

    public ExcelDataService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ExcelData> GetExcelDataList()
    {
        return _dbContext.ExcelData.ToList();
    }

    public void DeleteExcelData(int[] selectedIds)
    {
        var excelDataList = _dbContext.ExcelData.Where(e => selectedIds.Contains(e.Id)).ToList();
        _dbContext.ExcelData.RemoveRange(excelDataList);
        _dbContext.SaveChanges();
    }

    public List<ExcelData> GetSelectedExcelData(List<int> selectedIds)
    {
        // 查询选中的ExcelData记录
        var selectedData = _dbContext.ExcelData.Where(data => selectedIds.Contains(data.Id)).ToList();

        return selectedData;
    }
}