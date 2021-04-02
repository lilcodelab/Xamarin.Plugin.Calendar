using System;

namespace SampleApp.Model
{
    public class CalendarRangePickerResult
    {
        public bool IsSuccess { get; set; }
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
    }
}
