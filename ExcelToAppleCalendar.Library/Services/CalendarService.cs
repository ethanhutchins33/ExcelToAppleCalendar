using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace ExcelToAppleCalendar.Library.Services;

public class CalendarService : ICalendarService
{
    public IEnumerable<CalendarEvent> CreateEvents(IEnumerable<MatchEvent> matchEvents)
    {
        foreach (var matchEvent in matchEvents)
        {
            var homeOrAway = matchEvent.Home ? "(Home)" : "(Away)";

            yield return new CalendarEvent
            {
                Summary = matchEvent.OpponentTeam + " " + homeOrAway,
                DtStart = GetDate(matchEvent),
                Duration = new TimeSpan(3, 0, 0),
                Location = matchEvent.Address
            };
        }
    }

    public void CreateCalendar(IEnumerable<CalendarEvent> events)
    {
        var calendar = new Calendar();

        // Add the events to the calendar
        foreach (var ttEvent in events) calendar.Events.Add(ttEvent);

        // Serialize the calendar to an iCalendar file
        const string filePath = "Yeovil TT Calendar.ics";
        File.WriteAllText(filePath, new CalendarSerializer().SerializeToString(calendar));
    }

    private CalDateTime GetDate(MatchEvent match)
    {
        var mondayDate = new DateTime(match.WeekCommencingDate.Year, match.WeekCommencingDate.Month, match.WeekCommencingDate.Day);
        var actualDate =
            mondayDate.AddDays(GetDaysToAddFromMonday(match.DayOfWeek.ToString()));

        return new CalDateTime(actualDate.Year, actualDate.Month, actualDate.Day,
            match.StartTime.Hour,
            match.StartTime.Minute, 0);
    }

    private int GetDaysToAddFromMonday(string dayOfWeek)
    {
        return dayOfWeek switch
        {
            "Monday" => 0,
            "Tuesday" => 1,
            "Wednesday" => 2,
            "Thursday" => 3,
            "Friday" => 4,
            "Saturday" => 5,
            "Sunday" => 6,
            _ => throw new ArgumentException("Invalid day of the week.")
        };
    }
}