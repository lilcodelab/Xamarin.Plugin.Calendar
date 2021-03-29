using Rg.Plugins.Popup.Services;
using SampleApp.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CalendarPickerPopupViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public event Action<CalendarPickerResult> Closed;

        public ICommand ClearCommand => new Command(() =>
        {
            SelectedDate = DateTime.Today;
        });

        public ICommand SuccessCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarPickerResult() { IsSuccess = true, SelectedDate = SelectedDate });
            await PopupNavigation.Instance.PopAsync();
        });

        public ICommand CancelCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarPickerResult() { IsSuccess = false });
            await PopupNavigation.Instance.PopAsync();
        });

        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private DateTime _minimumDate = new DateTime(1900, 1, 1);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today;

        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }
    }
}
