using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Controls.MonthDayViews
{
    internal class SingleSelectionMonthDaysView : IMonthDaysView
    {
        private readonly MonthDaysView _parentView;

        internal SingleSelectionMonthDaysView(MonthDaysView parentView)
        {
            _parentView = parentView;
        }

        void IMonthDaysView.OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DayModel.IsSelected) || sender is not DayModel newSelected ||
                (_parentView.propertyChangedNotificationSupressions.TryGetValue(e.PropertyName, out bool isSuppressed) && isSuppressed))
                return;

            SelectSingleDate(newSelected);
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

                if (DateTime.Equals(dayModel.Date, _parentView.SelectedDate.Date))
                    _parentView.ChangePropertySilently(nameof(dayModel.IsSelected), () => dayModel.IsSelected = true);
                else
                    _parentView.ChangePropertySilently(nameof(dayModel.IsSelected), () => dayModel.IsSelected = false);
            }
        }

        private void SelectSingleDate(DayModel newSelected)
        {
            _parentView.SelectedDate = newSelected.Date;
        }
    }
}
