using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SimpleCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimplePage());
        }

        private async void AdvancedCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdvancedPage());
        }

        private async void RangeCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RangeSelectionPage());
        }

        private async void PickerPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new CalendarPickerPopup(async (calendarPickerResult) =>
            {
                string message = calendarPickerResult.IsSuccess ? $"Received date from popup: {calendarPickerResult.SelectedDate:dd/MM/yy}" : "Calendar Picker Canceled!";

                await App.Current.MainPage.DisplayAlert("Popup result", message, "Ok");
            }));
        }

        private async void RangePickerPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new CalendarRangePickerPopupSelectedDates(async (calendarPickerResult) =>
            {
                var message = "Calendar Range Piceker Canceled!";

                if (calendarPickerResult.IsSuccess && calendarPickerResult.SelectedDates?.Count > 0)
                {
                    var startDate = calendarPickerResult.SelectedDates[0];
                    var endDate = DateTime.MinValue;
                    foreach (DateTime date in calendarPickerResult.SelectedDates)
                    {
                        if (date < startDate)
                            startDate = date;
                        if (date > endDate)
                            endDate = date;
                    }
                    message = $"Received date range from popup: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
                }
                else if (calendarPickerResult.IsSuccess)
                    message = "Nothing is selected!";

                await App.Current.MainPage.DisplayAlert("Popup result", message, "Ok");
            }));
        }

        private async void RangeStartEndDatePickerPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new CalendarRangePickerPopup(async (calendarPickerResult) =>
            {
                var message = "Calendar Range Piceker Canceled!";

                if (calendarPickerResult.IsSuccess && calendarPickerResult.SelectedEndDate.HasValue && calendarPickerResult.SelectedEndDate.HasValue)
                {
                    var startDate = calendarPickerResult.SelectedStartDate;
                    var endDate = calendarPickerResult.SelectedEndDate;
                    message = $"Received date range from popup: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}";
                }
                else if (calendarPickerResult.IsSuccess)
                    message = "Nothing is selected!";

                await App.Current.MainPage.DisplayAlert("Popup result", message, "Ok");
            }));
        }

        private async void WeekViewCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeekViewPage());
        }

        private async void TwoWeekViewCalendar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TwoWeekViewPage());
        }
    }
}
