using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Plugin.Calendar.Controls.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls
{
    internal class RangedSelectionEngine : ISelectionEngine
    {
        private DateTime _rangeSelectionStartDate = DateTime.Today;
        private DateTime _rangeSelectionEndDate = DateTime.Today;

        public RangedSelectionEngine()
        { }

        bool ISelectionEngine.IsDateSelected(DateTime dateToCheck)
        {
            return DateTime.Compare(dateToCheck, _rangeSelectionEndDate.Date) <= 0 &&
                   DateTime.Compare(dateToCheck, _rangeSelectionStartDate.Date) >= 0;
        }

        List<DateTime> ISelectionEngine.PerformDateSelection(DateTime dateToSelect)
        {
            return SelectDateRange(dateToSelect);
        }

        internal List<DateTime> SelectDateRange(DateTime newSelected)
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

        void ISelectionEngine.UpdateDateSelection(List<DateTime> datesToSelect)
        {
            if(datesToSelect.Count > 0)
            {
                _rangeSelectionStartDate = datesToSelect[0];
                _rangeSelectionEndDate = datesToSelect[0];
            }

            foreach (var date in datesToSelect)
            {
                if (DateTime.Compare(date, _rangeSelectionStartDate) < 0)
                    _rangeSelectionStartDate = date;

                if (DateTime.Compare(_rangeSelectionEndDate, date) < 0)
                    _rangeSelectionEndDate = date;
            }
        }

        ICollection ISelectionEngine.GetSelectedEvents(EventCollection allEvents)
        {
            var listOfEvents = CreateRangeList();
            var wasSuccessful = allEvents.TryGetValues(listOfEvents, out var rangeEvents);

            return wasSuccessful ? rangeEvents : null;
        }

        string ISelectionEngine.GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture)
        {
            var startDateText = _rangeSelectionStartDate.ToString(selectedDateTextFormat, culture);
            var endDateText = _rangeSelectionEndDate.ToString(selectedDateTextFormat, culture);

            return $"{startDateText} - {endDateText}";
        }
    }
}
