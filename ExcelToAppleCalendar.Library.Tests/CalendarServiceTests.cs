using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class CalendarServiceTests
{
    private CalendarService _calendarService;

    private DateOnly _testWcDate;
    private DayOfWeek _testDayOfWeek;
    private string _testTeamName = string.Empty;
    private TimeOnly _testStartTime;
    private bool _testHome;
    private string _testAddress = string.Empty;

    [SetUp]
    public void Setup()
    {
        _calendarService = new CalendarService();

        _testWcDate = new DateOnly(2024, 01, 01);
        _testDayOfWeek = DayOfWeek.Tuesday;
        _testStartTime = new TimeOnly(19, 30, 00);
        _testTeamName = "Team Name";
        _testHome = true;
        _testAddress = "Address";
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Name()
    {
        var matchEvent = new MatchEvent
        {
            WeekCommencingDate = _testWcDate,
            DayOfWeek = _testDayOfWeek,
            StartTime = _testStartTime,
            OpponentTeam = _testTeamName,
            Home = _testHome,
            Address = _testAddress
        };

        var events = new List<MatchEvent> { matchEvent };

        var calendarEvent = _calendarService.CreateEvents(events).ToList().FirstOrDefault();
        ArgumentNullException.ThrowIfNull(calendarEvent);

        calendarEvent.Should().NotBeNull();
        calendarEvent.Summary.Should().Be(_testTeamName + " (Home)");
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Date()
    {
        var matchEvent = new MatchEvent
        {
            WeekCommencingDate = _testWcDate,
            DayOfWeek = _testDayOfWeek,
            StartTime = _testStartTime,
            OpponentTeam = _testTeamName,
            Home = _testHome,
            Address = _testAddress
        };

        var events = new List<MatchEvent> { matchEvent };

        var calendarEvent = _calendarService.CreateEvents(events).ToList().FirstOrDefault();
        ArgumentNullException.ThrowIfNull(calendarEvent);

        calendarEvent.Should().NotBeNull();
        calendarEvent.DtStart.Day.Should().Be(2);
    }
}