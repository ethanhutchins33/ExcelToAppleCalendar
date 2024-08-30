using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class ExcelDataReaderTests
{
    private IExcelDataReader _excelDataReader;

    [SetUp]
    public void Setup()
    {
        _excelDataReader = new ExcelDataReader();
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ReturnsExpectedResult()
    {
        const string excelFilePath = "test.xlsx";
        _excelDataReader.GetMatchEvents(excelFilePath);
    }

    [Test]
    public void ExcelDataReader_GetMatchEvents_ThrowsException_WhenExcelFileDoesNotExist()
    {
        Action act = () => _excelDataReader.GetMatchEvents("");

        act.Should().Throw<FileNotFoundException>();
    }
}