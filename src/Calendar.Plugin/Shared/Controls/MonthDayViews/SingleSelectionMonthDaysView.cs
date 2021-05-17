using System;
using System.ComponentModel;
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

        void IMonthDaysView.LoadDays(DateTime monthStart)
        {
            foreach (var dayView in _parentView.dayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;

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
