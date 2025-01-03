using ExcelToAppleCalendar.Library.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExcelToAppleCalendar.App;

public class App(
    ICalendarService calendarService,
    IExcelDataReader excelDataReader,
    IConfiguration configuration)
{
    public void Run()
    {
        Console.WriteLine("App Started");

        Console.WriteLine("Reading app configuration");
        var appSettings =
            configuration.GetSection("AppSettings").Get<AppConfiguration>();

        ArgumentNullException.ThrowIfNull(appSettings, nameof(appSettings));
        ArgumentException.ThrowIfNullOrEmpty(appSettings.InputFilePath,
            nameof(appSettings.InputFilePath));
        ArgumentException.ThrowIfNullOrEmpty(appSettings.OutputFilePath,
            nameof(appSettings.OutputFilePath));

        Console.WriteLine("Configuration loaded");

        // Get the data from the Excel file
        Console.WriteLine("Reading Excel file");
        var matchEvents = excelDataReader.GetMatchEvents(appSettings.InputFilePath).ToList();

        // Print the data to the console
        foreach (var rawData in matchEvents)
            Console.WriteLine(
                $"{rawData.WeekCommencingDate} {rawData.DayOfWeek} {rawData.OpponentTeam} Home:{rawData.Home} {rawData.StartTime} {rawData.Address}");

        // Create the calendar
        var events = calendarService.CreateEvents(matchEvents);

        calendarService.CreateCalendar(events);

        Console.WriteLine("iCalendar event saved");
    }
}