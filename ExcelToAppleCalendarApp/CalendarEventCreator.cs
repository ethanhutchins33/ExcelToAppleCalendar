using ExcelToAppleCalendarApp.Models;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace ExcelToAppleCalendarApp;

public class CalendarEventCreator
{
    public IEnumerable<CalendarEvent> CreateEvents(IEnumerable<RowEventDto> rowData)
    {
        foreach (var row in rowData)
        {
            var homeOrAway = row.Home ? "(Home)" : "(Away)";
            
            yield return new CalendarEvent
            {
                Summary = row.Team + " " + homeOrAway,
                DtStart = GetDate(row),
                Duration = new TimeSpan(3,0,0),
                Location = row.Postcode,
            };
        }
    }

    private CalDateTime GetDate(RowEventDto row)
    {
        var month = -1;
        if (row.YearMonth != null)
        {
            month = GetIntFromMonth(row.YearMonth);
        }

        var year = month < 8 ? 2024 : 2023;
        var mondayDate = new DateTime(year, month, row.WeekCommencing);
        var actualDate = mondayDate.AddDays(GetDaysToAddFromMonday(row.DayOfWeek.ToString()));
        
        return new CalDateTime(actualDate.Year, actualDate.Month, actualDate.Day, row.StartTime.Hour,
            row.StartTime.Minute, 0);
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