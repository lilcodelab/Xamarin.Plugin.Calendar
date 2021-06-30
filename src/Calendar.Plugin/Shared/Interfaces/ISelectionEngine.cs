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
    public interface ISelectionEngine
    {
        /// <summary>
        /// Method to get formatted selected dates text
        /// </summary>
        string GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture);

        /// <summary>
        /// Method to get all events for currently selected dates
        /// </summary>
        /// <param name="allEvents">EventCollection with events</param>
        /// <param name="selectedEvents">returns ICollection of events for selected period</param>
        /// <returns>returns true if there are events in allEvents collection for selected period</returns>
        bool TryGetSelectedEvents(EventCollection allEvents, out ICollection selectedEvents);

        /// <summary>
        /// Method to check is selected day
        /// </summary>
        /// <param name="dateToCheck">Date to check is selected</param>
        /// <returns>true if dateToCheck is slected</returns>
        bool IsDateSelected(DateTime dateToCheck);

        /// <summary>
        /// Method to perform selection
        /// </summary>
        List<DateTime> PerformDateSelection(DateTime dateToSelect);

        /// <summary>
        /// Method to selectedDates when changed from code
        /// </summary>
        /// <param name="datesToSelect"></param>
        void UpdateDateSelection(List<DateTime> datesToSelect);
    }
}
