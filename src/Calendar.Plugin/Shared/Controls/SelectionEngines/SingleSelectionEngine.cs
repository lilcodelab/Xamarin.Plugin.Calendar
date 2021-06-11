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
        private DateTime? _selectedDate;

        internal SingleSelectionEngine()
        { }

        string ISelectionEngine.GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture)
        {
            return _selectedDate?.ToString(selectedDateTextFormat, culture);
        }

        ICollection ISelectionEngine.GetSelectedEvents(EventCollection allEvents)
        {
            if (!_selectedDate.HasValue)
                return null;

            var wasSuccessful = allEvents.TryGetValue(_selectedDate.Value, out var dayEvents);

            return wasSuccessful ? dayEvents : null;
        }

        bool ISelectionEngine.IsDateSelected(DateTime dateToCheck)
        {
            return Equals(dateToCheck, _selectedDate);
        }

        List<DateTime> ISelectionEngine.PerformDateSelection(DateTime dateToSelect)
        {
            if (dateToSelect == _selectedDate)
            {
                _selectedDate = null;
                return new List<DateTime>();
            }

            SelectSingleDate(dateToSelect);

            return new List<DateTime> { dateToSelect };
        }

        void ISelectionEngine.UpdateDateSelection(List<DateTime> datesToSelect)
        {
            if (datesToSelect.Count > 0)
                _selectedDate = datesToSelect[0];
            else
                _selectedDate = null;
        }

        private void SelectSingleDate(DateTime? dateToSelect)
        {
            _selectedDate = dateToSelect;
        }
    }
}
