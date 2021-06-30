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
        private DateTime _maximumDate = DateTime.Today.AddYears(1);

        private DateTime _minimumDate = DateTime.Today.AddYears(-1);

        private DateTime _monthYear = DateTime.Today;

        // If you do with _selectedDates then set _startDate & _endDate to null
        private DateTime? _selectedStartDate = DateTime.Today.AddDays(-5);
        private DateTime? _selectedEndDate = DateTime.Today.AddDays(5);

        // If you do with startDate and endDate then set _selectedDates to null
        private List<DateTime> _selectedDates = null;

        // TODO create new class when we refactor sample app.
        //private List<DateTime> _selectedDates = new List<DateTime> {
        //    DateTime.Today.AddDays(1),
        //    DateTime.Today.AddDays(2),
        //    DateTime.Today.AddDays(3)
        //};

        public CalendarRangePickerPopupViewModel()
        {
            //SelectedDates = new List<DateTime>
            //{
            //    DateTime.Today,
            //    DateTime.Today.AddDays(2),
            //    DateTime.Today.AddDays(4),
            //    DateTime.Today.AddDays(6),
            //    DateTime.Today.AddDays(5),
            //};
        }

        public event Action<CalendarRangePickerResult> Closed;

        public ICommand CancelCommand => new Command(async () =>
        {
            Closed?.Invoke(new CalendarRangePickerResult() { IsSuccess = false });
            await PopupNavigation.Instance.PopAsync();
        });

        public ICommand ClearCommand => new Command(() =>
                {
                    //SelectedDates = null;
                    SelectedEndDate = null;
                    SelectedStartDate = null;
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

        public DateTime? SelectedStartDate 
        {
            get => _selectedStartDate; 
            set => SetProperty(ref _selectedStartDate, value); 
        }

        public DateTime? SelectedEndDate
        {
            get => _selectedEndDate;
            set => SetProperty(ref _selectedEndDate, value);
        }

        public ICommand SuccessCommand => new Command(async () =>
            {
                Closed?.Invoke(new CalendarRangePickerResult()
                {
                    IsSuccess = true,
                    SelectedDates = SelectedDates,
                    SelectedStartDate = SelectedStartDate,
                    SelectedEndDate = SelectedEndDate
                });
                await PopupNavigation.Instance.PopAsync();
            });
    }
}
