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
        private DateTime? _rangeSelectionEndDate = null;
        private DateTime? _rangeSelectionStartDate = null;

        public RangedSelectionEngine()
        { }

        string ISelectionEngine.GetSelectedDateText(string selectedDateTextFormat, CultureInfo culture)
        {
            if (_rangeSelectionStartDate.HasValue)
            {
                var startDateText = _rangeSelectionStartDate.Value.ToString(selectedDateTextFormat, culture);
                var endDateText = _rangeSelectionEndDate.Value.ToString(selectedDateTextFormat, culture);
                return $"{startDateText} - {endDateText}";
            }
            return string.Empty;
        }

        ICollection ISelectionEngine.GetSelectedEvents(EventCollection allEvents)
        {
            var listOfEvents = CreateRangeList();
            var wasSuccessful = allEvents.TryGetValues(listOfEvents, out var rangeEvents);

            return wasSuccessful ? rangeEvents : null;
        }

        bool ISelectionEngine.IsDateSelected(DateTime dateToCheck)
        {
            if (!_rangeSelectionStartDate.HasValue)
                return false;

            return DateTime.Compare(dateToCheck, _rangeSelectionEndDate.Value.Date) <= 0 &&
                   DateTime.Compare(dateToCheck, _rangeSelectionStartDate.Value.Date) >= 0;
        }

        List<DateTime> ISelectionEngine.PerformDateSelection(DateTime dateToSelect)
        {
            return SelectDateRange(dateToSelect);
        }

        void ISelectionEngine.UpdateDateSelection(List<DateTime> datesToSelect)
        {
            if (datesToSelect?.Count > 0)
            {
                _rangeSelectionStartDate = datesToSelect[0];
                _rangeSelectionEndDate = datesToSelect[0];

                foreach (var date in datesToSelect)
                {
                    if (DateTime.Compare(date, _rangeSelectionStartDate.Value) < 0)
                        _rangeSelectionStartDate = date;

                    if (DateTime.Compare(_rangeSelectionEndDate.Value, date) < 0)
                        _rangeSelectionEndDate = date;
                }
            }
        }

        internal List<DateTime> SelectDateRange(DateTime? newSelected)
        {
            if (_rangeSelectionStartDate is null || !Equals(_rangeSelectionStartDate, _rangeSelectionEndDate))
                SelectFirstIntervalBorder(newSelected);
            else
                SelectSecondIntervalBorder(newSelected);

            return CreateRangeList();
        }

        private List<DateTime> CreateRangeList()
        {
            var rangeList = new List<DateTime>();
            if (_rangeSelectionStartDate.HasValue && _rangeSelectionEndDate.HasValue)
            {
                for (var currentDate = _rangeSelectionStartDate; DateTime.Compare(currentDate.Value, _rangeSelectionEndDate.Value) <= 0; currentDate = currentDate.Value.AddDays(1))
                    rangeList.Add(currentDate.Value);
            }

            return rangeList;
        }

        internal List<DateTime> GetDateRange() => CreateRangeList();

        private void SelectFirstIntervalBorder(DateTime? newSelected)
        {
            _rangeSelectionStartDate = newSelected?.Date;
            _rangeSelectionEndDate = newSelected?.Date;
        }

        internal DateTime? RangeSelectionStartDate => _rangeSelectionStartDate;
        internal DateTime? RangeSelectionEndDate => _rangeSelectionEndDate;

        private void SelectSecondIntervalBorder(DateTime? newSelected)
        {
            if (DateTime.Compare(newSelected.Value.Date, _rangeSelectionStartDate.Value.Date) <= 0)
                _rangeSelectionStartDate = newSelected.Value.Date;
            else
                _rangeSelectionEndDate = newSelected.Value.Date;
        }
    }
}
