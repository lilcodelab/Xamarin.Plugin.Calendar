using System;
using System.ComponentModel;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    /// <summary>
    /// Interface for different selection implementations within MonthDaysView
    /// </summary>
    interface IMonthDaysView
    {
        /// <summary>
        /// Method that is called when the DayModel is changed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e);

        /// <summary>
        /// Method to load selected days in the calendar properly
        /// </summary>
        internal void LoadDays(DateTime monthStart);
    }
}
