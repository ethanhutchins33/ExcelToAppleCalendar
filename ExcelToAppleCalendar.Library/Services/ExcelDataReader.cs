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
        var worksheet = package.Workbook.Worksheets.First();

        var rowCount = worksheet.Rows.Count();
        Console.WriteLine($"Row Count: {rowCount}");

        for (var i = 2; i <= rowCount; i++)
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

    // private int GetRowsWithDataCount(ExcelWorksheet worksheet)
    // {
    //     int count;
    //     foreach (var worksheetRow in worksheet.Rows)
    //     {
    //         if(worksheetRow.)
    //         count += 1;
    //     }
    // }
}