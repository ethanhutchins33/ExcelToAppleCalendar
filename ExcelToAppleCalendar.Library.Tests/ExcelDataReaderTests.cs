using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class ExcelDataReaderTests
{
    private ExcelDataReader _excelDataReader;
    private const string ExcelFilePath = "./TestFiles/TestFile.xlsx";

    [SetUp]
    public void Setup()
    {
        _excelDataReader = new ExcelDataReader();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedWCDate()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.WeekCommencingDate.Should().Be(new DateOnly(2024, 9, 30));
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedDayOfWeek()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.DayOfWeek.Should().Be(DayOfWeek.Wednesday);
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedTeamName()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.OpponentTeam.Should().Be("Team Name");
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedHomeAway()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.Home.Should().BeFalse();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedStartTime()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.StartTime.Should().Be(new TimeOnly(19, 30, 0));
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedAddress()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();
        result.Address.Should().Be("AddressValue");
    }
}