using System;
using System.ComponentModel;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class RangeSelectionMonthDaysView : IMonthDaysView
    {
        private readonly MonthDaysView _parentView;

        public RangeSelectionMonthDaysView(MonthDaysView parentView)
        {
            _parentView = parentView;
        }

        void IMonthDaysView.OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DayModel.IsSelected) || sender is not DayModel newSelected ||
                (_parentView.propertyChangedNotificationSupressions.TryGetValue(e.PropertyName, out bool isSuppressed) && isSuppressed))
                return;

            SelectDateRange(newSelected);
        }

        void IMonthDaysView.LoadDays(DateTime monthStart)
        {
            foreach (var dayView in _parentView.dayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;

                if (DateTime.Compare(dayModel.Date, _parentView.RangeSelectionEndDate.Date) <= 0 && 
                    DateTime.Compare(dayModel.Date, _parentView.RangeSelectionStartDate.Date) >= 0 ||
                    DateTime.Equals(dayModel.Date, _parentView.RangeSelectionStartDate.Date))
                    _parentView.ChangePropertySilently(nameof(dayModel.IsSelected), () => dayModel.IsSelected = true);
                else
                    _parentView.ChangePropertySilently(nameof(dayModel.IsSelected), () => dayModel.IsSelected = false);
            }
        }

        private void SelectDateRange(DayModel newSelected)
        {
            if (Equals(_parentView.RangeSelectionStartDate, _parentView.RangeSelectionEndDate))
                SelectSecondIntervalBorder(newSelected);
            else
                SelectFirstIntervalBorder(newSelected);
        }

        private void SelectFirstIntervalBorder(DayModel newSelected)
        {
            _parentView.RangeSelectionStartDate = newSelected.Date;
            _parentView.RangeSelectionEndDate = newSelected.Date;
        }

        private void SelectSecondIntervalBorder(DayModel newSelected)
        {
            if(DateTime.Compare(newSelected.Date, _parentView.RangeSelectionStartDate.Date) <= 0)
                _parentView.RangeSelectionStartDate = newSelected.Date;
            else
                _parentView.RangeSelectionEndDate = newSelected.Date;
        }
    }
}
