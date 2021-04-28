using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using SampleApp.Model;
using Rg.Plugins.Popup.Services;
using SampleApp.Views;

namespace SampleApp.ViewModels
{
    public class RangeSelectionPageViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public ICommand OpenRangePickerCommand => new Command(async () =>
        {
            await PopupNavigation.Instance.PushAsync(new CalendarRangePickerPopup(async (calendarPickerResult) =>
            {
                string message = calendarPickerResult.IsSuccess ?
                    $"Received date range from popup: {calendarPickerResult.SelectedStartDate:dd.MM.yyyy} - {calendarPickerResult.SelectedEndDate:dd.MM.yyyy}" :
                    "Calendar Range Picker Canceled!";

                await App.Current.MainPage.DisplayAlert("Popup result", message, "Ok");
            }));
        });

        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public RangeSelectionPageViewModel() : base()
        {
            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-1)] = new List<AdvancedEventModel>(GenerateEvents(5, "Cool")),
                [DateTime.Now.AddDays(-2)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(-2))),
                [DateTime.Now.AddDays(-4)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Super Cool")),
                [DateTime.Now.AddDays(-5)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(-6)] = new DayEventCollection<AdvancedEventModel>(Color.Purple, Color.Purple)
                {
                    new AdvancedEventModel { Name = "Cool event1", Description = "This is Cool event1's description!", Starting= new DateTime()},
                    new AdvancedEventModel { Name = "Cool event2", Description = "This is Cool event2's description!", Starting= new DateTime()}
                },
                [DateTime.Now.AddDays(-10)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(1)] = new List<AdvancedEventModel>(GenerateEvents(2, "Boring")),
                [DateTime.Now.AddDays(4)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(8)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(9)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool H")),
                [DateTime.Now.AddDays(10)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool X")),
                [DateTime.Now.AddDays(16)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool B")),
                [DateTime.Now.AddDays(20)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool A")),

            };

            MonthYear = MonthYear.AddMonths(1);            
        }

        private IEnumerable<AdvancedEventModel> GenerateEvents(int count, string name)
        {
            return Enumerable.Range(1, count).Select(x => new AdvancedEventModel
            {
                Name = $"{name} event{x}",
                Description = $"This is {name} event{x}'s description!",
                Starting = new DateTime(2000, 1, 1, (x * 2) % 24, (x * 3) % 60, 0)
            });
        }

        private IEnumerable<AdvancedEventModel> GenerateEvents(int count, string name, DateTime timeOfEvent)
        {
            return Enumerable.Range(1, count).Select(x => new AdvancedEventModel
            {
                Name = $"{name} event{x}",
                Description = $"This is {name} event{x}'s description!",
                Starting = new DateTime(timeOfEvent.Year, timeOfEvent.Month, timeOfEvent.Day, (x * 2) % 24, (x * 3) % 60, 0)
            });
        }

        public EventCollection Events { get; }

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

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is AdvancedEventModel eventModel)
            {
                var title = $"Selected: {eventModel.Name}";
                var message = $"Starts: {eventModel.Starting:HH:mm}{Environment.NewLine}Details: {eventModel.Description}";
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
            }
        }
    }
}
