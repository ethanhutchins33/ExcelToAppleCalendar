# Excel to Apple Calendar App

This is a C# console application that imports my table tennis match data from an Excel file and generates an iCalendar. The iCalendar file can be imported into calendar applications such as Google Calendar, Microsoft Outlook, or Apple Calendar.

## Requirements

.NET 10.0 or later

## Usage

1. Clone the repository to your local machine.
1. Open the solution file (`ExcelToAppleCalendar.sln`) in Visual Studio (or another IDE).
1. Build the solution to restore the NuGet packages.
1. Put the Excel file you want to convert in `ExcelToAppleCalendar.App/DataFiles/` (or update the path).
1. Update `ExcelToAppleCalendar.App/appsettings.json` to set `InputFilePath` and `OutputFilePath`.
1. Run the application (`dotnet run --project ExcelToAppleCalendar.App`).
1. The iCalendar file will be saved to the configured `OutputFilePath`.
