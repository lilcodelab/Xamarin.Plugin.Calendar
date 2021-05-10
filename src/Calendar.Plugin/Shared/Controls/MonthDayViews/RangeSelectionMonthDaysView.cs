using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

        void IMonthDaysView.LoadDays()
        {
            var monthStart = new DateTime(_parentView.DisplayedMonthYear.Year, _parentView.DisplayedMonthYear.Month, 1);
            var addDays = ((int)_parentView.Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

            if (addDays > 0)
                addDays -= 7;

            foreach (var dayView in _parentView.dayViews)
            {
                var currentDate = monthStart.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate.Date;
                dayModel.DayViewSize = _parentView.DayViewSize;
                dayModel.DayViewCornerRadius = _parentView.DayViewCornerRadius;
                dayModel.DayTappedCommand = _parentView.DayTappedCommand;
                dayModel.DaysLabelStyle = _parentView.DaysLabelStyle;
                dayModel.EventIndicatorType = _parentView.EventIndicatorType;
                dayModel.IsThisMonth = currentDate.Month == _parentView.DisplayedMonthYear.Month;
                dayModel.OtherMonthIsVisible = _parentView.OtherMonthDayIsVisible;
                dayModel.HasEvents = _parentView.Events.ContainsKey(currentDate);
                dayModel.IsDisabled = currentDate < _parentView.MinimumDate || currentDate > _parentView.MaximumDate;

                _parentView.AssignIndicatorColors(ref dayModel);

                if (DateTime.Compare(currentDate.Date, _parentView.RangeSelectionEndDate.Date) <= 0 && 
                    DateTime.Compare(currentDate.Date, _parentView.RangeSelectionStartDate.Date) >= 0 ||
                    DateTime.Equals(currentDate.Date, _parentView.RangeSelectionStartDate.Date))
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