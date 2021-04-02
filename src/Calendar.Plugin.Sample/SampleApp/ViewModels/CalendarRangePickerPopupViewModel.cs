using Rg.Plugins.Popup.Services;
using SampleApp.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CalendarRangePickerPopupViewModel : BasePageViewModel
    {
        public event Action<CalendarRangePickerResult> Closed;

        public ICommand ClearCommand => new Command(() =>
        {
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
        });

        public ICommand SuccessCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult()
            {
                IsSuccess = true,
                SelectedStartDate = SelectedStartDate,
                SelectedEndDate = SelectedEndDate,
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

        private DateTime _selectedStartDate = DateTime.Today;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set => SetProperty(ref _selectedStartDate, value);
        }

        private DateTime _selectedEndDate = DateTime.Today;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set => SetProperty(ref _selectedEndDate, value);
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
