using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Tesseract;
using Calendar = Ical.Net.Calendar;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;


namespace iCalGenerator;
public class WeeklySchedule
{
	public class ScheduleItem
	{
		public string DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}

    public string ExtractTextFromImage(string imagePath)
    {
        string preprocessedImagePath = Path.Combine(Path.GetDirectoryName(imagePath), "preprocessed_" + Path.GetFileName(imagePath));
        PreprocessImage(imagePath, preprocessedImagePath);

        using var ocrEngine = new TesseractEngine(@"./tessdata", "eng+fra", EngineMode.TesseractOnly);
        using var img = Pix.LoadFromFile(preprocessedImagePath);
        using var page = ocrEngine.Process(img);
        return page.GetText();
    }

    public void PreprocessImage(string inputPath, string outputPath)
    {
        using var image = SixLabors.ImageSharp.Image.Load(inputPath);

        // Convert the image to grayscale
        image.Mutate(x => x.Grayscale());

        // Resize the image to double its original dimensions
        image.Mutate(x => x.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions
        {
            Size = new SixLabors.ImageSharp.Size(image.Width * 2, image.Height * 2),
            Sampler = SixLabors.ImageSharp.Processing.KnownResamplers.NearestNeighbor
        }));

        // Apply a threshold to convert the image to black and white
        image.Mutate(x => x.BinaryThreshold(0.7f));

        image.Save(outputPath);
    }

    public List<ScheduleItem> ParseScheduleText(string scheduleText)
    {
        var scheduleItems = new List<ScheduleItem>();

        // Split the text into lines
        var lines = scheduleText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        // Define a mapping for French day names to their English counterparts
        var dayMapping = new Dictionary<string, string>
    {
        { "Dimanche", "Sunday" },
        { "Lundi", "Monday" },
        { "Mardi", "Tuesday" },
        { "Mercredi", "Wednesday" },
        { "Jeudi", "Thursday" },
        { "Vendredi", "Friday" },
        { "Samedi", "Saturday" }
    };

        string currentDay = null;
        TimeSpan currentTime = TimeSpan.Zero;

        foreach (var line in lines)
        {
            // Check if the line contains a French day name
            var dayMatch = dayMapping.Keys.FirstOrDefault(day => line.Contains(day));
            if (dayMatch != null)
            {
                currentDay = dayMapping[dayMatch];
                continue;
            }

            // Check if the line contains a time (e.g., "8:00" or "22:00")
            var timeMatch = Regex.Match(line, @"\b\d{1,2}:\d{2}\b");
            if (timeMatch.Success)
            {
                currentTime = TimeSpan.Parse(timeMatch.Value);
                continue;
            }

            // Check if the line contains a class code (e.g., "MAT265", "ELE216", "ELE105", or "GIA410")
            var classMatch = Regex.Match(line, @"\b[A-Z]{3}\d{3}\b");
            if (classMatch.Success && currentDay != null && currentTime != TimeSpan.Zero)
            {
                var scheduleItem = new ScheduleItem
                {
                    DayOfWeek = currentDay,
                    StartTime = currentTime,
                    EndTime = currentTime.Add(TimeSpan.FromMinutes(30)) // Assuming each class is 30 minutes long
                };

                scheduleItems.Add(scheduleItem);

                // Update the current time for the next class
                currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
            }
        }

        return scheduleItems;
    }


    public void CreateWeeklyICalFile(string outputFilePath, List<ScheduleItem> scheduleItems, DateTime startDate, DateTime endDate)
	{
		var calendar = new Calendar();

        foreach (var item in scheduleItems)
        {
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
			{
				if (date.ToString("dddd",CultureInfo.InvariantCulture).Equals(item.DayOfWeek, StringComparison.OrdinalIgnoreCase))
				{
					var calendarEvent = new CalendarEvent
					{
						Summary = "Weekly Event",
						Start = new CalDateTime(date.Date + item.StartTime),
						End = new CalDateTime(date.Date + item.EndTime),
						IsAllDay = false
					};

					calendar.Events.Add(calendarEvent);
				}
			}
        }

        var serializer = new CalendarSerializer();
        var serializedCalendar = serializer.SerializeToString(calendar);
        File.WriteAllText(outputFilePath, serializedCalendar);
    }
}
