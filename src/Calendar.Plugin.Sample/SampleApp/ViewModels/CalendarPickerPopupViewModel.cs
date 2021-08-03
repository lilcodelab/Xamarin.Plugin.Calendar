using Rg.Plugins.Popup.Services;
using SampleApp.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Enums;

namespace SampleApp.ViewModels
{
    public class CalendarPickerPopupViewModel : BasePageViewModel
    {
        public event Action<CalendarPickerResult> Closed;

        public CalendarPickerPopupViewModel() : base()
        {
            SelectedDate = new DateTime(2021, 6, 13);
        }

        public ICommand ClearCommand => new Command(() =>
        {
            SelectedDate = null;
        });

        public ICommand SuccessCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarPickerResult() { IsSuccess = SelectedDate.HasValue, SelectedDate = SelectedDate });

            await PopupNavigation.Instance.PopAsync();
        });

        public ICommand CancelCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarPickerResult() { IsSuccess = false });
            await PopupNavigation.Instance.PopAsync();
        });

        private DateTime _shownDate = DateTime.Today;

        public DateTime ShownDate
        {
            get => _shownDate;
            set => SetProperty(ref _shownDate, value);
        }

        private WeekLayout _calendarLayout = WeekLayout.Month;

        public WeekLayout CalendarLayout
        {
            get => _calendarLayout;
            set => SetProperty(ref _calendarLayout, value);
        }

        private DateTime? _selectedDate;

        public DateTime? SelectedDate
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
