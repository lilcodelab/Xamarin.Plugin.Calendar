using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Plugin.Calendar.Models;

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
    }
}
