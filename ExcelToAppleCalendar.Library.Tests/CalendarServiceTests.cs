using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class CalendarServiceTests
{
    private CalendarService _calendarService;

    [SetUp]
    public void Setup()
    {
        _calendarService = new CalendarService();
    }

    [Test]
    public void CalendarService_Creates_Events()
    {
        const string testYearMonth = "January";
        const int testWeekCommencing = 1;
        const DayOfWeek testDayOfWeek = DayOfWeek.Sunday;
        var testStartTime = new TimeOnly(19, 30, 00);
        const string testTeam = "Team Name";
        const bool testHome = true;
        const string testPostcode = "Postcode";
        const string testAddress = "Address";


        var matchEvent = new MatchEvent
        {
            YearMonth = testYearMonth,
            WeekCommencing = testWeekCommencing,
            DayOfWeek = testDayOfWeek,
            StartTime = testStartTime,
            Team = testTeam,
            Home = testHome,
            Postcode = testPostcode,
            Address = testAddress
        };

        var events = new List<MatchEvent> { matchEvent };

        var calendarEvent = _calendarService.CreateEvents(events).ToList().FirstOrDefault();
        ArgumentNullException.ThrowIfNull(calendarEvent);

        calendarEvent.Should().NotBeNull();
        calendarEvent.Name.Should().Be(testTeam + " (home)");
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
}