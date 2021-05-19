using System;
using System.Collections.Generic;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class SingleSelectionMonthDaysView : IMonthDaysView
    {
        private DateTime _selectedDate = DateTime.Today;

        internal SingleSelectionMonthDaysView()
        { }

        bool IMonthDaysView.IsSelected(DateTime dateToCheck)
        {
            return DateTime.Equals(dateToCheck, _selectedDate);
        }

        List<DateTime> IMonthDaysView.PerformSelection(DateTime dateToSelect)
        {
            SelectSingleDate(dateToSelect);
            return new List<DateTime> { dateToSelect };
        }

        private void SelectSingleDate(DateTime dateToSelect)
        {
            _selectedDate = dateToSelect;
        }
    }
}
