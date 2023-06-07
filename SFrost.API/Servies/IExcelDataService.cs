public interface IExcelDataService
{
    List<ExcelData> GetExcelDataList();
    void DeleteExcelData(int[] selectedIds);
    List<ExcelData> GetSelectedExcelData(List<int> selectedIds);

}