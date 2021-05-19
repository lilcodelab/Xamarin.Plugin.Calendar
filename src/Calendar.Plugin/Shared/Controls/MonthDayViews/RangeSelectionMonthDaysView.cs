using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class RangeSelectionMonthDaysView : IMonthDaysView
    {
        private DateTime _rangeSelectionStartDate = DateTime.Today;
        private DateTime _rangeSelectionEndDate = DateTime.Today.AddDays(7);

        public RangeSelectionMonthDaysView()
        { }

        bool IMonthDaysView.IsSelected(DateTime dateToCheck)
        {
            return DateTime.Compare(dateToCheck, _rangeSelectionEndDate.Date) <= 0 &&
                   DateTime.Compare(dateToCheck, _rangeSelectionStartDate.Date) >= 0;
        }

        List<DateTime> IMonthDaysView.PerformSelection(DateTime dateToSelect)
        {
            return SelectDateRange(dateToSelect);
        }

        private List<DateTime> SelectDateRange(DateTime newSelected)
        {
            if (!DateTime.Equals(_rangeSelectionStartDate, _rangeSelectionEndDate))
                SelectFirstIntervalBorder(newSelected);
            else
                SelectSecondIntervalBorder(newSelected);

            return CreateRangeList();
        }

        private void SelectFirstIntervalBorder(DateTime newSelected)
        {
            _rangeSelectionStartDate = newSelected.Date;
            _rangeSelectionEndDate = newSelected.Date;
        }

        private void SelectSecondIntervalBorder(DateTime newSelected)
        {
            if(DateTime.Compare(newSelected.Date, _rangeSelectionStartDate.Date) <= 0)
                _rangeSelectionStartDate = newSelected.Date;
            else
                _rangeSelectionEndDate = newSelected.Date;
        }

        private List<DateTime> CreateRangeList()
        {
            var rangeList = new List<DateTime>();

            for (var currentDate = _rangeSelectionStartDate; DateTime.Compare(currentDate, _rangeSelectionEndDate) <= 0; currentDate = currentDate.AddDays(1))
                rangeList.Add(currentDate);

            return rangeList;
        }
    }
}
