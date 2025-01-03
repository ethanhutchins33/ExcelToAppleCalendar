namespace ExcelToAppleCalendar.Library.Models;

public class MatchEvent
{
    public DateOnly WeekCommencingDate { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string? OpponentTeam { get; set; } = "";
    public bool Home { get; set; }
    public TimeOnly StartTime { get; set; }
    public string? Address { get; set; }
}