using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class ExcelDataReaderTests
{
    private IExcelDataReader _excelDataReader;
    const string ExcelFilePath = "./DataFiles/TestFile.xlsx";

    [SetUp]
    public void Setup()
    {
        _excelDataReader = new ExcelDataReader();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedTeamName()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.Team.Should().Be("Team Name");
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedWeekCommencing()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.WeekCommencing.Should().Be(1);
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedYearMonth()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.YearMonth.Should().Be("January");
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedStartTime()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.StartTime.Should().Be(new TimeOnly(19, 30, 0));
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedDayOfWeek()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.DayOfWeek.Should().Be(DayOfWeek.Monday);
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedHomeAway()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.Home.Should().BeFalse();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedPostCode()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.Postcode.Should().Be("PostCodeValue");
    }
}