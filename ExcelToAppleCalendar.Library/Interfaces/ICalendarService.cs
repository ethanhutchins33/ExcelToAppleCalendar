using ExcelToAppleCalendar.Library.Models;
using Ical.Net.CalendarComponents;

namespace ExcelToAppleCalendar.Library.Interfaces;

public interface ICalendarService
{
    public IEnumerable<CalendarEvent> CreateEvents(IEnumerable<MatchEvent> matchEvents);
    public void CreateCalendar(IEnumerable<CalendarEvent> events);
}