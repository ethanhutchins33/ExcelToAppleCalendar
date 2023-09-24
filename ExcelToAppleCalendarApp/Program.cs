using ExcelToAppleCalendarApp;
using Ical.Net;
using Ical.Net.Serialization;

const string excelFilePath = "./DataFiles/Yeovil.xlsx";

var data = ExcelDataReader.GetData(excelFilePath);

foreach (var rawData in data)
{
    Console.WriteLine($"{rawData.Team} {rawData.YearMonth} {rawData.WeekCommencing} {rawData.DayOfWeek} {rawData.StartTime} {rawData.Postcode}");
}

var eventCreator = new CalendarEventCreator();

var calendar = new Calendar();
var events = eventCreator.CreateEvents(data);

// Add the event to the calendar
foreach (var ttEvent in events)
{
    calendar.Events.Add(ttEvent);
}

// Serialize the calendar to an iCalendar file
const string filePath = "Yeovil TT Calendar.ics";
File.WriteAllText(filePath, new CalendarSerializer().SerializeToString(calendar));

Console.WriteLine("iCalendar event saved to " + filePath);