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
                Summary = matchEvent.Team + " " + homeOrAway,
                DtStart = GetDate(matchEvent),
                Duration = new TimeSpan(3, 0, 0),
                Location = matchEvent.Postcode
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
        var month = -1;
        if (match.YearMonth != null) month = GetIntFromMonth(match.YearMonth);

        var year = month < 8 ? 2024 : 2023;
        var mondayDate = new DateTime(year, month, match.WeekCommencing);
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

    private int GetIntFromMonth(string month)
    {
        return month switch
        {
            "January" => 1,
            "February" => 2,
            "March" => 3,
            "April" => 4,
            "May" => 5,
            "June" => 6,
            "July" => 7,
            "August" => 8,
            "September" => 9,
            "October" => 10,
            "November" => 11,
            "December" => 12,
            _ => throw new ArgumentException("Invalid month name.")
        };
    }
}