using System;
using System.Collections.Generic;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    /// <summary>
    /// Interface for different selection implementations within MonthDaysView
    /// </summary>
    interface IMonthDaysView
    {
        /// <summary>
        /// Method to load selected days in the calendar properly
        /// </summary>
        internal bool IsSelected(DateTime dateToCheck);

        /// <summary>
        /// Method to perform event selection
        /// </summary>
        internal List<DateTime> PerformSelection(DateTime dateToSelect);

        /// <summary>
        /// Method to update selectedDates when changed from code
        /// </summary>
        /// <param name="datesToSelect"></param>
        internal void UpdateSelection(List<DateTime> datesToSelect);
    }
}
