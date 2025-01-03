using ExcelToAppleCalendar.Library.Interfaces;
using FakeItEasy;
using Microsoft.Extensions.Configuration;

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

        A.CallTo(() => configuration.GetSection("AppSettings"))
            .Returns(configurationSection);
        A.CallTo(() => configurationSection.Get<IAppConfiguration>())
            .Returns(testConfiguration);

        _app = new App(calendarService, excelDataReader,
            configuration);
    }
}