using System.Globalization;
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
            Console.WriteLine($"Reading row #{i}");

            // If row data is empty, skip row
            if (worksheet.Cells[i, 2].Value == null) continue;
            Console.WriteLine($"Row #{i} data was empty, skipping row...");

            var weekCommencingDate = DateOnly.TryParse(worksheet.Cells[i, 1].Text,
                CultureInfo.CurrentCulture, out var date)
                ? date
                : throw new FormatException();
            Console.WriteLine($"Week Commencing Date: {weekCommencingDate}");

            var dayOfWeek =
                Enum.TryParse(worksheet.Cells[i, 2].Text, true, out DayOfWeek day)
                    ? day
                    : DayOfWeek.Sunday;
            Console.WriteLine($"Day of Week: {dayOfWeek}");

            var opponentTeam = worksheet.Cells[i, 3].Text;
            Console.WriteLine($"Opponent Team: {opponentTeam}");

            var homeOrAway = worksheet.Cells[i, 4].Text == "Home";
            Console.WriteLine($"At Home?: {homeOrAway}");

            var startTime = TimeOnly.Parse(worksheet.Cells[i, 5].Text);
            Console.WriteLine($"Start Time: {startTime}");

            var address = worksheet.Cells[i, 6].Text;
            Console.WriteLine($"Address: {address}");

            MatchEvent matchEvent = new()
            {
                WeekCommencingDate = weekCommencingDate,
                DayOfWeek = dayOfWeek,
                OpponentTeam = opponentTeam,
                Home = homeOrAway,
                StartTime = startTime,
                Address = address
            };

            yield return matchEvent;
        }
    }
}