using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class ExcelDataReaderTests
{
    private ExcelDataReader _excelDataReader = null!;
    private const string ExcelFilePath = "./TestFiles/TestFile.xlsx";

    [SetUp]
    public void Setup()
    {
        _excelDataReader = new ExcelDataReader();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ThrowsFileNotFoundException_WhenFileDoesNotExist()
    {
        var nonExistentFilePath = "./TestFiles/NonExistentFile.xlsx";
        var action = () => _excelDataReader.GetMatchEvents(nonExistentFilePath).ToList();

        action.Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsMultipleMatchEvents()
    {
        var results = _excelDataReader.GetMatchEvents(ExcelFilePath).ToList();

        results.Should().NotBeEmpty().And.HaveCountGreaterThanOrEqualTo(1);
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_AllResults_HaveNonNullValues()
    {
        var results = _excelDataReader.GetMatchEvents(ExcelFilePath).ToList();

        results.Should().AllSatisfy(result =>
        {
            result.WeekCommencingDate.Should().NotBe(default);
            result.DayOfWeek.Should().NotBe(default);
            result.OpponentTeam.Should().NotBeNullOrEmpty();
            result.StartTime.Should().NotBe(default);
            result.Address.Should().NotBeNullOrEmpty();
        });
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_FirstEvent_HasCorrectValues()
    {
        var result = _excelDataReader.GetMatchEvents(ExcelFilePath).First();

        result.WeekCommencingDate.Should().Be(new DateOnly(2024, 9, 30));
        result.DayOfWeek.Should().Be(DayOfWeek.Wednesday);
        result.OpponentTeam.Should().Be("Team Name");
        result.Home.Should().BeFalse();
        result.StartTime.Should().Be(new TimeOnly(19, 30, 0));
        result.Address.Should().Be("AddressValue");
    }
}