using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.Interfaces
{
    /// <summary>
    /// Interface for different selection implementations within MonthDaysView
    /// </summary>
    interface ISelectionEngine
    {
        /// <summary>
        /// Method to load selected days in the calendar properly
        /// </summary>
        internal bool IsDateSelected(DateTime? dateToCheck);

        /// <summary>
        /// Method to perform event selection
        /// </summary>
        internal List<DateTime> PerformDateSelection(DateTime? dateToSelect);

        /// <summary>
        /// Method to update selectedDates when changed from code
        /// </summary>
        /// <param name="datesToSelect"></param>
        internal void UpdateDateSelection(List<DateTime> datesToSelect);

        /// <summary>
        /// Method to get all events for currently selected dates
        /// </summary>
        internal ICollection GetSelectedEvents(EventCollection allEvents);

        /// <summary>
        /// Method to get formatted selected dates text
        /// </summary>
        internal string GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture);
    }
}
