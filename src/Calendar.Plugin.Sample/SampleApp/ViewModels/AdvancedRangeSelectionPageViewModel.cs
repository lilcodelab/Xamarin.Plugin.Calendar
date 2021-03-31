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
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));

        public ICommand OpenRangePickerCommand => new Command(async () =>
        {
            await PopupNavigation.Instance.PushAsync(new CalendarRangePickerPopup(async (calendarPickerResult) =>
            {
                string message = calendarPickerResult.IsSuccess ?
                    $"Received date range from popup: {calendarPickerResult.SelectedStartDate:dd.MM.yyyy} - {calendarPickerResult.SelectedEndDate:dd.MM.yyyy}" :
                    "Calendar Range Picker Canceled!";

                await App.Current.MainPage.DisplayAlert("Popup result", message, "Ok");
                Console.WriteLine(message);
            }));
        });

        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public RangeSelectionPageViewModel() : base()
        {
            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-3)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool")),
                [DateTime.Now.AddDays(-6)] = new DayEventCollection<AdvancedEventModel>(Color.Purple, Color.Purple)
                {
                    new AdvancedEventModel { Name = "Cool event1", Description = "This is Cool event1's description!", Starting= new DateTime()},
                    new AdvancedEventModel { Name = "Cool event2", Description = "This is Cool event2's description!", Starting= new DateTime()}
                }
            };

            //Adding a day with a different dot color
            Events.Add(DateTime.Now.AddDays(-2), new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(-2))) { EventIndicatorColor = Color.Blue, EventIndicatorSelectedColor = Color.Blue });
            Events.Add(DateTime.Now.AddDays(-4), new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool")) { EventIndicatorColor = Color.Green, EventIndicatorSelectedColor = Color.White });
            Events.Add(DateTime.Now.AddDays(-5), new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool")) { EventIndicatorColor = Color.Orange, EventIndicatorSelectedColor = Color.Orange });

            // with add method
            Events.Add(DateTime.Now.AddDays(-1), new List<AdvancedEventModel>(GenerateEvents(5, "Cool")));

            // with indexer
            Events[DateTime.Now] = new List<AdvancedEventModel>(GenerateEvents(2, "Boring"));

            MonthYear = MonthYear.AddMonths(1);

            Task.Delay(5000).ContinueWith(_ =>
            {
                // indexer - update later
                Events[DateTime.Now] = new ObservableCollection<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now));

                // add later
                Events.Add(DateTime.Now.AddDays(3), new List<AdvancedEventModel>(GenerateEvents(5, "Cool", DateTime.Now.AddDays(3))));

                // indexer later
                Events[DateTime.Now.AddDays(10)] = new List<AdvancedEventModel>(GenerateEvents(10, "Boring", DateTime.Now.AddDays(10)));

                // add later
                Events.Add(DateTime.Now.AddDays(15), new List<AdvancedEventModel>(GenerateEvents(10, "Cool")));


                Task.Delay(3000).ContinueWith(t =>
                {
                    MonthYear = MonthYear.AddMonths(-2);

                    // get observable collection later
                    var todayEvents = Events[DateTime.Now] as ObservableCollection<AdvancedEventModel>;

                    // insert/add items to observable collection
                    todayEvents.Insert(0, new AdvancedEventModel { Name = "Cool event insert", Description = "This is Cool event's description!", Starting = new DateTime() });
                    todayEvents.Add(new AdvancedEventModel { Name = "Cool event add", Description = "This is Cool event's description!", Starting = new DateTime() });

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
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

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
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

        private static async Task DayTapped(DateTime date)
        {
            var message = $"Received tap event from date: {date}";
            await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
            Console.WriteLine(message);
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is AdvancedEventModel eventModel)
            {
                var title = $"Selected: {eventModel.Name}";
                var message = $"Starts: {eventModel.Starting:HH:mm}{Environment.NewLine}Details: {eventModel.Description}";
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
                Console.WriteLine(message);
            }
        }
    }
}
