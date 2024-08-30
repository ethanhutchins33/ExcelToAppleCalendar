namespace ExcelToAppleCalendar.App;

public interface IAppConfiguration
{
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
}