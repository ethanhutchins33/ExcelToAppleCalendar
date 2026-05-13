# ExcelToAppleCalendar

A .NET console application that converts table tennis match schedules from an Excel spreadsheet into an iCalendar (`.ics`) file, ready to import into Apple Calendar, Google Calendar, Microsoft Outlook, or any other standards-compliant calendar application.

## Features

- Reads match schedule data from `.xlsx` files via [EPPlus](https://github.com/EPPlusSoftware/EPPlus)
- Generates RFC 5545-compliant `.ics` files via [Ical.Net](https://github.com/rianjs/ical.net)
- Configurable input and output file paths via `appsettings.json`
- Each event includes opponent name, home/away indicator, start time, and venue address
- Events default to a 3-hour duration

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/ethanhutchins33/ExcelToAppleCalendar.git
cd ExcelToAppleCalendar
```

### 2. Prepare your Excel file

Place your `.xlsx` file in `ExcelToAppleCalendar.App/DataFiles/`. The spreadsheet must have the following columns (starting from row 2):

| Column | Field              | Format / Example        |
|--------|--------------------|-------------------------|
| A      | Week Commencing    | `dd/MM/yyyy`            |
| B      | Day of Week        | `Monday`, `Tuesday`, …  |
| C      | Opponent Team      | `Riverside TTC`         |
| D      | Home / Away        | `Home` or `Away`        |
| E      | Start Time         | `19:30`                 |
| F      | Venue Address      | `123 Main St, London`   |

### 3. Configure the application

Edit `ExcelToAppleCalendar.App/appsettings.json`:

```json
{
  "AppSettings": {
    "InputFilePath": "./DataFiles/DataFile.xlsx",
    "OutputFilePath": "./TT_Calendar.ics"
  }
}
```

### 4. Run the application

```bash
dotnet run --project ExcelToAppleCalendar.App
```

The `.ics` file will be written to the path set in `OutputFilePath`.

### 5. Import into your calendar

Open the generated `.ics` file with your calendar application of choice (Apple Calendar, Google Calendar, Outlook, etc.) to import all match events.

## Project Structure

```
ExcelToAppleCalendar.sln
├── ExcelToAppleCalendar.App/          # Console application entry point
│   ├── appsettings.json               # Input/output path configuration
│   └── DataFiles/                     # Default location for Excel input files
├── ExcelToAppleCalendar.App.Tests/    # Tests for the App project
├── ExcelToAppleCalendar.Library/      # Core library (Excel reader, calendar service)
│   ├── Models/                        # MatchEvent model
│   ├── Services/                      # ExcelDataReader, CalendarService
│   └── Interfaces/                    # IExcelDataReader, ICalendarService
└── ExcelToAppleCalendar.Library.Tests/ # Tests for the Library project
```

## Running Tests

```bash
dotnet test
```

## Contributing

Contributions are welcome. Please open an issue to discuss your proposed change before submitting a pull request.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -m 'Add my feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Open a pull request
