using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using FakeItEasy;
using FluentAssertions;
using Ical.Net.CalendarComponents;
using Microsoft.Extensions.Configuration;

namespace ExcelToAppleCalendar.App.Tests;

public class AppTests
{
    private ICalendarService _calendarService = null!;
    private IExcelDataReader _excelDataReader = null!;

    [SetUp]
    public void Setup()
    {
        _calendarService = A.Fake<ICalendarService>();
        _excelDataReader = A.Fake<IExcelDataReader>();

        A.CallTo(() => _excelDataReader.GetMatchEvents(A<string>._))
            .Returns([]);
        A.CallTo(() => _calendarService.CreateEvents(A<IEnumerable<MatchEvent>>._))
            .Returns([]);
    }

    private App BuildApp(string? inputFilePath = "input.xlsx", string? outputFilePath = "output.ics")
    {
        var settings = new Dictionary<string, string?>();
        if (inputFilePath != null) settings["AppSettings:InputFilePath"] = inputFilePath;
        if (outputFilePath != null) settings["AppSettings:OutputFilePath"] = outputFilePath;

        IConfiguration config = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();

        return new App(_calendarService, _excelDataReader, config);
    }

    private App BuildAppWithEmptyConfig()
    {
        IConfiguration config = new ConfigurationBuilder().Build();
        return new App(_calendarService, _excelDataReader, config);
    }

    [Test]
    public void App_Run_Calls_GetMatchEvents_With_Correct_Input_File_Path()
    {
        BuildApp().Run();

        A.CallTo(() => _excelDataReader.GetMatchEvents("input.xlsx"))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void App_Run_Calls_CreateEvents()
    {
        BuildApp().Run();

        A.CallTo(() => _calendarService.CreateEvents(A<IEnumerable<MatchEvent>>._))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void App_Run_Calls_CreateCalendar()
    {
        BuildApp().Run();

        A.CallTo(() => _calendarService.CreateCalendar(A<IEnumerable<CalendarEvent>>._))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void App_Run_Throws_ArgumentNullException_When_AppSettings_Is_Null()
    {
        var app = BuildAppWithEmptyConfig();

        var act = app.Run;

        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void App_Run_Throws_ArgumentException_When_InputFilePath_Is_Empty()
    {
        var app = BuildApp(inputFilePath: "");

        var act = app.Run;

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void App_Run_Throws_ArgumentException_When_OutputFilePath_Is_Empty()
    {
        var app = BuildApp(outputFilePath: "");

        var act = app.Run;

        act.Should().Throw<ArgumentException>();
    }
}