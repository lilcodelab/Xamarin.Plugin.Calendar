using Rg.Plugins.Popup.Services;
using SampleApp.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CalendarRangePickerPopupViewModel : BasePageViewModel
    {
        public event Action<CalendarRangePickerResult> Closed;

        public ICommand ClearCommand => new Command(() =>
        {
            SelectedDates = new List<DateTime> { 
                DateTime.Today, 
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(2),
                DateTime.Today.AddDays(-3),
                DateTime.Today.AddDays(-4),
                DateTime.Today.AddDays(-5),
                DateTime.Today.AddDays(-6),
            };
        });

        public ICommand SuccessCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult()
            {
                IsSuccess = true,
                SelectedDates = SelectedDates,
            });
            await PopupNavigation.Instance.PopAsync();
        });

        public ICommand CancelCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult() { IsSuccess = false });
            await PopupNavigation.Instance.PopAsync();
        });

        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        private List<DateTime> _selectedDates = new List<DateTime> { 
            DateTime.Today, 
            DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(2),
                DateTime.Today.AddDays(-3),
                DateTime.Today.AddDays(-4),
                DateTime.Today.AddDays(-5),
                DateTime.Today.AddDays(-6),
        };
        public List<DateTime> SelectedDates
        {
            get => _selectedDates;
            set => SetProperty(ref _selectedDates, value);
        }

        private DateTime _minimumDate = DateTime.Today.AddYears(-1);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today.AddYears(1);

        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }
    }
}
