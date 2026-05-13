using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;
using Ical.Net.DataTypes;

namespace ExcelToAppleCalendar.Library.Tests;

public class CalendarServiceTests
{
    private CalendarService _calendarService;

    // Week commencing date is a Monday (2024-01-01)
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

    private MatchEvent BuildMatchEvent(
        DayOfWeek? dayOfWeek = null,
        bool? home = null,
        string? address = null) =>
        new()
        {
            WeekCommencingDate = _testWcDate,
            DayOfWeek = dayOfWeek ?? _testDayOfWeek,
            StartTime = _testStartTime,
            OpponentTeam = _testTeamName,
            Home = home ?? _testHome,
            Address = address ?? _testAddress
        };

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Name()
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent()]).First();

        calendarEvent.Summary.Should().Be(_testTeamName + " (Home)");
    }

    [Test]
    public void CalendarService_Creates_Events_With_Away_Suffix_When_Away()
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent(home: false)]).First();

        calendarEvent.Summary.Should().Be(_testTeamName + " (Away)");
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Date()
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent()]).First();

        // WC date is Monday 2024-01-01; Tuesday is day 2
        calendarEvent.DtStart!.Day.Should().Be(2);
    }

    [TestCase(DayOfWeek.Monday, 1)]
    [TestCase(DayOfWeek.Tuesday, 2)]
    [TestCase(DayOfWeek.Wednesday, 3)]
    [TestCase(DayOfWeek.Thursday, 4)]
    [TestCase(DayOfWeek.Friday, 5)]
    [TestCase(DayOfWeek.Saturday, 6)]
    [TestCase(DayOfWeek.Sunday, 7)]
    public void CalendarService_Creates_Events_With_Correct_Date_For_Each_Day(DayOfWeek dayOfWeek, int expectedDay)
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent(dayOfWeek: dayOfWeek)]).First();

        calendarEvent.DtStart!.Day.Should().Be(expectedDay);
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Duration()
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent()]).First();

        calendarEvent.Duration.Should().Be(Duration.FromHours(3));
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Location()
    {
        var calendarEvent = _calendarService.CreateEvents([BuildMatchEvent(address: "Test Venue")]).First();

        calendarEvent.Location.Should().Be("Test Venue");
    }

    [Test]
    public void CalendarService_Creates_Events_Returns_Empty_For_Empty_Input()
    {
        var result = _calendarService.CreateEvents([]);

        result.Should().BeEmpty();
    }

    [Test]
    public void CalendarService_Creates_Events_Returns_Correct_Count_For_Multiple_Events()
    {
        var result = _calendarService.CreateEvents([BuildMatchEvent(), BuildMatchEvent(), BuildMatchEvent()]);

        result.Should().HaveCount(3);
    }

    [Test]
    public void CalendarService_Creates_Events_Throws_For_Invalid_Day_Of_Week()
    {
        var matchEvent = BuildMatchEvent(dayOfWeek: (DayOfWeek)99);

        var act = () => _calendarService.CreateEvents([matchEvent]).ToList();

        act.Should().Throw<ArgumentException>();
    }
}