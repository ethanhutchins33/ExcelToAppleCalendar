using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using ExcelToAppleCalendar.Library.Services;

namespace ExcelToAppleCalendar.Library.Tests;

public class CalendarServiceTests
{
    private ICalendarService _calendarService;

    [SetUp]
    public void Setup()
    {
        _calendarService = new CalendarService();
    }

    [Test]
    public void CalendarService_Creates_Calendar()
    {
        var matchEvent = new MatchEvent
        {
            YearMonth = "yearMonth",
            WeekCommencing = 0,
            DayOfWeek = DayOfWeek.Sunday,
            StartTime = default,
            Team = null,
            Home = false,
            Postcode = null,
            Address = null
        };

        var events = new List<MatchEvent> { matchEvent };

        //_calendarService.CreateCalendar(events);
    }

    [Test]
    public void CalendarService_Creates_Events()
    {
    }
}