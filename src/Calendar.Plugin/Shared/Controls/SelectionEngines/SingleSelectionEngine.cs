using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Plugin.Calendar.Controls.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class SingleSelectionEngine : ISelectionEngine
    {
        private DateTime _selectedDate = DateTime.Today;

        internal SingleSelectionEngine()
        { }

        bool ISelectionEngine.IsDateSelected(DateTime dateToCheck)
        {
            return DateTime.Equals(dateToCheck, _selectedDate);
        }

        List<DateTime> ISelectionEngine.PerformDateSelection(DateTime dateToSelect)
        {
            SelectSingleDate(dateToSelect);
            return new List<DateTime> { dateToSelect };
        }

        private void SelectSingleDate(DateTime dateToSelect)
        {
            _selectedDate = dateToSelect;
        }

        void ISelectionEngine.UpdateDateSelection(List<DateTime> datesToSelect)
        {
            _selectedDate = datesToSelect[0];
        }

        ICollection ISelectionEngine.GetSelectedEvents(EventCollection allEvents)
        {
            var wasSuccessful = allEvents.TryGetValue(_selectedDate, out var dayEvents);

            return wasSuccessful ? dayEvents : null;
        }

        string ISelectionEngine.GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture)
        {
            return _selectedDate.ToString(selectedDateTextFormat, culture);
        }
    }
}
