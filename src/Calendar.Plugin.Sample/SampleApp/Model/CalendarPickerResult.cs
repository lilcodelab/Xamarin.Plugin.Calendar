using System;

namespace SampleApp.Model
{
    public class CalendarPickerResult
    {
        public bool IsSuccess { get; set; }

        public DateTime? SelectedDate { get; set; }
    }
}
