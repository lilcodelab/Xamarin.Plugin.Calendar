using System;
using System.Collections.Generic;

namespace SampleApp.Model
{
    public class CalendarRangePickerResult
    {
        public bool IsSuccess { get; set; }
        public List<DateTime> SelectedDates { get; set; } = new List<DateTime>();
        public DateTime? SelectedStartDate { get; set; }
        public DateTime? SelectedEndDate { get; set; }
    }
}
