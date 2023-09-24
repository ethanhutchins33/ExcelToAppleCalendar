namespace ExcelToAppleCalendarApp.Models;

public class RowEventDto
{
    public string? YearMonth { get; set; }
    public int WeekCommencing { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public string? Team { get; set; }
    public bool Home { get; set; }
    public string? Postcode { get; set; }
}