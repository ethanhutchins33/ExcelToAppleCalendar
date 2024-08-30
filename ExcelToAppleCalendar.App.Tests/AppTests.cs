using ExcelToAppleCalendar.Library.Interfaces;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExcelToAppleCalendar.App.Tests;

public class AppTests
{
    private App _app;

    [SetUp]
    public void Setup()
    {
        var calendarService = A.Fake<ICalendarService>();
        var excelDataReader = A.Fake<IExcelDataReader>();
        IAppConfiguration testConfiguration = new TestConfiguration();
        var configurationSection = A.Fake<IConfigurationSection>();
        var configuration = A.Fake<IConfiguration>();
        var logger = A.Fake<ILogger<App>>();

        A.CallTo(() => configuration.GetSection("AppSettings"))
            .Returns(configurationSection);
        A.CallTo(() => configurationSection.Get<IAppConfiguration>())
            .Returns(testConfiguration);

        _app = new App(calendarService, excelDataReader,
            configuration, logger);
    }

    [Test]
    public void App_CalendarService_CalendarIsNotNull()
    {
        _app.Run();
    }
}