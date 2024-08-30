namespace ExcelToAppleCalendar.App.Tests;

public class TestConfiguration : IAppConfiguration
{
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
}