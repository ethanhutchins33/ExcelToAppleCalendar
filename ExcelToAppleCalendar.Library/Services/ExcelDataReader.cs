using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Models;
using OfficeOpenXml;

namespace ExcelToAppleCalendar.Library.Services;

public class ExcelDataReader : IExcelDataReader
{
    public IEnumerable<MatchEvent> GetMatchEvents(string filePath)
    {
        var file = new FileInfo(filePath);

        if (!file.Exists)
        {
            Console.WriteLine("File Not Found");
            throw new FileNotFoundException();
        }

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage(file);
        var worksheet = package.Workbook.Worksheets["Fixtures"];

        for (var i = 2; i < 13; i++)
        {
            MatchEvent matchEvent = new()
            {
                YearMonth = worksheet.Cells[i, 1].Text,
                WeekCommencing =
                    int.Parse(worksheet.Cells[i, 2].Text
                        .Remove(worksheet.Cells[i, 2].Text.Length - 2)),
                DayOfWeek = Enum.TryParse(worksheet.Cells[i, 3].Text, true, out DayOfWeek day)
                    ? day
                    : DayOfWeek.Sunday,
                StartTime = TimeOnly.Parse(worksheet.Cells[i, 4].Text),
                Team = worksheet.Cells[i, 5].Text,
                Home = worksheet.Cells[i, 6].Text == "Home",
                Postcode = worksheet.Cells[i, 7].Text
            };

            yield return matchEvent;
        }
    }
}