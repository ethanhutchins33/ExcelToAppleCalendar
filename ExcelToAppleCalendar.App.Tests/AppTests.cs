using ExcelToAppleCalendar.Library.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace ExcelToAppleCalendar.App.Tests;

public class AppTests
{
    private ICalendarService _calendarService = null!;
    private IExcelDataReader _excelDataReader = null!;
    private IConfiguration _configuration = null!;
    private App _app = null!;

    [SetUp]
    public void Setup()
    {
        _calendarService = A.Fake<ICalendarService>();
        _excelDataReader = A.Fake<IExcelDataReader>();
        _configuration = A.Fake<IConfiguration>();
        _app = new App(_calendarService, _excelDataReader, _configuration);
    }

    [Test]
    public void App_Instantiation_WithValidDependencies_Succeeds()
    {
        _app.Should().NotBeNull();
    }

    [Test]
    public void App_Can_Be_Created_With_Fakes()
    {
        var act = () => new App(_calendarService, _excelDataReader, _configuration);
        act.Should().NotThrow();
    }

    [Test]
    public void App_Is_Correct_Type_After_Instantiation()
    {
        _app.Should().BeOfType<App>();
    }
}