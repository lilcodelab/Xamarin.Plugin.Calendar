using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class SingleSelectionType : IChosenSelectionType
    {
        private DateTime _selectedDate = DateTime.Today;

        internal SingleSelectionType()
        { }

        bool IChosenSelectionType.IsDateSelected(DateTime dateToCheck)
        {
            return DateTime.Equals(dateToCheck, _selectedDate);
        }

        List<DateTime> IChosenSelectionType.PerformDateSelection(DateTime dateToSelect)
        {
            SelectSingleDate(dateToSelect);
            return new List<DateTime> { dateToSelect };
        }

        private void SelectSingleDate(DateTime dateToSelect)
        {
            _selectedDate = dateToSelect;
        }

        void IChosenSelectionType.UpdateDateSelection(List<DateTime> datesToSelect)
        {
            _selectedDate = datesToSelect[0];
        }

        ICollection IChosenSelectionType.GetSelectedEvents(EventCollection allEvents)
        {
            var wasSuccessful = allEvents.TryGetValue(_selectedDate, out var dayEvents);

            return wasSuccessful ? dayEvents : null;
        }

        string IChosenSelectionType.GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture)
        {
            return _selectedDate.ToString(selectedDateTextFormat, culture);
        }
    }
}
