using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Plugin.Calendar.Controls.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.SelectionEngines
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

        bool ISelectionEngine.TryGetSelectedEvents(EventCollection allEvents, out ICollection selectedEvents)
        {
            if (_selectedDate.HasValue)
                return allEvents.TryGetValue(_selectedDate.Value, out selectedEvents);

            selectedEvents = null;
            return false;
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
            if (datesToSelect?.Count > 0)
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
