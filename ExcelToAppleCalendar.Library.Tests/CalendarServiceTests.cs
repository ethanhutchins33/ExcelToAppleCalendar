using ExcelToAppleCalendar.Library.Models;
using ExcelToAppleCalendar.Library.Services;
using FluentAssertions;

namespace ExcelToAppleCalendar.Library.Tests;

public class CalendarServiceTests
{
    private CalendarService _calendarService = null!;
    private DateOnly _testWcDate;
    private TimeOnly _testStartTime;

    [SetUp]
    public void Setup()
    {
        _calendarService = new CalendarService();
        _testWcDate = new DateOnly(2024, 01, 01);
        _testStartTime = new TimeOnly(19, 30, 00);
    }

    private MatchEvent CreateTestEvent(
        DayOfWeek dayOfWeek = DayOfWeek.Tuesday,
        string teamName = "Team Name",
        bool home = true,
        string address = "Address")
    {
        return new MatchEvent
        {
            WeekCommencingDate = _testWcDate,
            DayOfWeek = dayOfWeek,
            StartTime = _testStartTime,
            OpponentTeam = teamName,
            Home = home,
            Address = address
        };
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Name()
    {
        var matchEvent = CreateTestEvent();
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.Summary.Should().Be("Team Name (Home)");
    }

    [TestCase(true, "Team Name (Home)")]
    [TestCase(false, "Team Name (Away)")]
    public void CalendarService_Creates_Events_With_Correct_HomeAway_Label(bool isHome, string expectedSummary)
    {
        var matchEvent = CreateTestEvent(home: isHome);
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.Summary.Should().Be(expectedSummary);
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Date()
    {
        var matchEvent = CreateTestEvent();
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.DtStart!.Day.Should().Be(2);
    }

    [TestCase(DayOfWeek.Monday, 1)]
    [TestCase(DayOfWeek.Tuesday, 2)]
    [TestCase(DayOfWeek.Wednesday, 3)]
    [TestCase(DayOfWeek.Thursday, 4)]
    [TestCase(DayOfWeek.Friday, 5)]
    [TestCase(DayOfWeek.Saturday, 6)]
    [TestCase(DayOfWeek.Sunday, 7)]
    public void CalendarService_Creates_Events_With_Correct_Day_For_AllDaysOfWeek(DayOfWeek dayOfWeek, int expectedDay)
    {
        var matchEvent = CreateTestEvent(dayOfWeek: dayOfWeek);
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.DtStart!.Day.Should().Be(expectedDay);
    }

    [Test]
    public void CalendarService_Creates_Events_With_Correct_Location()
    {
        var matchEvent = CreateTestEvent(address: "Test Address");
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.Location.Should().Be("Test Address");
    }

    [Test]
    public void CalendarService_Creates_Events_With_3Hour_Duration()
    {
        var matchEvent = CreateTestEvent();
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.Duration.Should().NotBeNull();
    }

    [TestCase(19, 30)]
    [TestCase(14, 0)]
    [TestCase(20, 15)]
    public void CalendarService_Creates_Events_With_Correct_Time(int hour, int minute)
    {
        var startTime = new TimeOnly(hour, minute);
        var matchEvent = CreateTestEvent();
        matchEvent.StartTime = startTime;
        
        var calendarEvent = _calendarService.CreateEvents([matchEvent]).FirstOrDefault();

        calendarEvent.Should().NotBeNull();
        calendarEvent!.DtStart!.Hour.Should().Be(hour);
        calendarEvent.DtStart.Minute.Should().Be(minute);
    }

    [Test]
    public void CalendarService_Creates_Multiple_Events()
    {
        var matchEvent1 = CreateTestEvent(dayOfWeek: DayOfWeek.Monday, teamName: "Team 1");
        var matchEvent2 = CreateTestEvent(dayOfWeek: DayOfWeek.Wednesday, teamName: "Team 2", home: false);

        var calendarEvents = _calendarService.CreateEvents([matchEvent1, matchEvent2]).ToList();

        calendarEvents.Should().HaveCount(2);
        calendarEvents[0].Summary.Should().Be("Team 1 (Home)");
        calendarEvents[1].Summary.Should().Be("Team 2 (Away)");
    }

    [Test]
    public void CalendarService_Creates_Events_With_EmptyEnumerable_Returns_Empty()
    {
        var calendarEvents = _calendarService.CreateEvents(new List<MatchEvent>()).ToList();

        calendarEvents.Should().BeEmpty();
    }

    [Test]
    public void CalendarService_CreateCalendar_CreatesFile()
    {
        var calendarEvent = new Ical.Net.CalendarComponents.CalendarEvent
        {
            Summary = "Test Event",
            DtStart = new Ical.Net.DataTypes.CalDateTime(2024, 1, 1, 19, 30, 0),
            Duration = Ical.Net.DataTypes.Duration.FromHours(3),
            Location = "Test Location"
        };

        _calendarService.CreateCalendar([calendarEvent]);

        File.Exists("TT Calendar.ics").Should().BeTrue();
        File.Delete("TT Calendar.ics");
    }

    [Test]
    public void CalendarService_CreateCalendar_CreatesValidICalFile()
    {
        var calendarEvent = new Ical.Net.CalendarComponents.CalendarEvent
        {
            Summary = "Test Event",
            DtStart = new Ical.Net.DataTypes.CalDateTime(2024, 1, 1, 19, 30, 0),
            Duration = Ical.Net.DataTypes.Duration.FromHours(3),
            Location = "Test Location"
        };

        _calendarService.CreateCalendar([calendarEvent]);

        var fileContent = File.ReadAllText("TT Calendar.ics");
        fileContent.Should().Contain("BEGIN:VCALENDAR");
        fileContent.Should().Contain("BEGIN:VEVENT");
        fileContent.Should().Contain("SUMMARY:Test Event");
        fileContent.Should().Contain("LOCATION:Test Location");
        fileContent.Should().Contain("END:VCALENDAR");

        File.Delete("TT Calendar.ics");
    }
}