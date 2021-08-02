using Rg.Plugins.Popup.Services;
using SampleApp.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CalendarRangePickerPopupSelectedDatesViewModel : BasePageViewModel
    {
        private DateTime _maximumDate = DateTime.Today.AddYears(1);

        private DateTime _minimumDate = DateTime.Today.AddYears(-1);

        private DateTime _monthYear = DateTime.Today;

        private List<DateTime> _selectedDates = null;

        public CalendarRangePickerPopupSelectedDatesViewModel()
        {
            SelectedDates = new List<DateTime>
            {
                DateTime.Today,
                DateTime.Today.AddDays(6),
            };
        }

        public event Action<CalendarRangePickerResult> Closed;

        public ICommand CancelCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult() { IsSuccess = false });
            await PopupNavigation.Instance.PopAsync();
        });

        public ICommand ClearCommand => new Command(() =>
        {
            SelectedDates = null;
        });

        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        public List<DateTime> SelectedDates
        {
            get => _selectedDates;
            set => SetProperty(ref _selectedDates, value);
        }

        public ICommand SuccessCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult()
            {
                IsSuccess = true,
                SelectedDates = SelectedDates
            });
            await PopupNavigation.Instance.PopAsync();
        });
    }
}
