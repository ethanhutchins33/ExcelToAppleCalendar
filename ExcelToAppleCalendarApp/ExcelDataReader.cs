using ExcelToAppleCalendarApp.Models;
using OfficeOpenXml;

namespace ExcelToAppleCalendarApp;

public static class ExcelDataReader
{
    public static IEnumerable<RowEventDto> GetData(string filePath)
    {
        var file = new FileInfo(filePath);
        if (file.Exists)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(file);
            var worksheet = package.Workbook.Worksheets["Fixtures"];

            for (var i = 2; i < 13; i++)
            {
                DayOfWeek day;
                RowEventDto rowEventDto = new()
                {
                    YearMonth = worksheet.Cells[i, 1].Text,
                    WeekCommencing =
                        int.Parse(worksheet.Cells[i, 2].Text.Remove(worksheet.Cells[i, 2].Text.Length - 2)),
                    DayOfWeek = DayOfWeek.TryParse(worksheet.Cells[i, 3].Text, true, out day)
                        ? day
                        : DayOfWeek.Sunday,
                    StartTime = TimeOnly.Parse(worksheet.Cells[i, 4].Text),
                    Team = worksheet.Cells[i, 5].Text,
                    Home = worksheet.Cells[i, 6].Text == "Home",
                    Postcode = worksheet.Cells[i, 7].Text
                };

                yield return rowEventDto;
            }
        }
        else
        {
            Console.WriteLine("File Not Found");
        }
    }
}