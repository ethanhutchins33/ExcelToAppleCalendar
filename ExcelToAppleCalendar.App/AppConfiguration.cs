namespace ExcelToAppleCalendar.App;

public class AppConfiguration : IAppConfiguration
{
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
}