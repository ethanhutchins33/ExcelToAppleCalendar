using ExcelToAppleCalendar.Library.Models;

namespace ExcelToAppleCalendar.Library.Interfaces;

public interface IExcelDataReader
{
    public IEnumerable<MatchEvent> GetMatchEvents(string filePath);
}