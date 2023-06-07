using System.ComponentModel.DataAnnotations;

public class ExcelData
{
    [Key]
    public int Id { get; set; }

    public string? ModuleName { get; set; }
    public string? TaskItem { get; set; }
    public DateTime StartTime { set; get; }
    public DateTime EndTime { set; get; }
    public string? TaskOwner { set; get; }
    public string? TaskNotes { set; get; }

    public DateTime ImportTime { set; get; }
    // 添加其他属性，以匹配您的Excel文件中的列
}
