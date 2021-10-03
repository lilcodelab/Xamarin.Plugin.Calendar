using SampleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Models;

namespace SampleApp.ViewModels
{
    public class RangeSelectionPageViewModel : BasePageViewModel
    {
        private DateTime? _selectedEndDate = DateTime.Today.AddDays(2);

        private DateTime _shownDate = DateTime.Today;

        private WeekLayout _calendarLayout = WeekLayout.Month;

        private List<DateTime> _selectedDates = new();

        private DateTime? _selectedStartDate = DateTime.Today.AddDays(-9);

        public RangeSelectionPageViewModel() : base()
        {
            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-1)] = new List<AdvancedEventModel>(GenerateEvents(5, "Cool", DateTime.Now.AddDays(-1))),
                [DateTime.Now.AddDays(-2)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(-2))),
                [DateTime.Now.AddDays(-4)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Super Cool", DateTime.Now.AddDays(-4))),
                [DateTime.Now.AddDays(-5)] = new DayEventCollection<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(-5))),
                [DateTime.Now.AddDays(-6)] = new DayEventCollection<AdvancedEventModel>(Color.Purple, Color.Purple)
                {
                    new AdvancedEventModel { Name = "Cool event1", Description = "This is Cool event1's description!", Starting= DateTime.Now.AddDays(-6)},
                    new AdvancedEventModel { Name = "Cool event2", Description = "This is Cool event2's description!", Starting= DateTime.Now.AddDays(-6)}
                },
                [DateTime.Now.AddDays(-10)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(-10))),
                [DateTime.Now.AddDays(1)] = new List<AdvancedEventModel>(GenerateEvents(2, "Boring", DateTime.Now.AddDays(1))),
                [DateTime.Now.AddDays(4)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool", DateTime.Now.AddDays(4))),
                [DateTime.Now.AddDays(8)] = new List<AdvancedEventModel>(GenerateEvents(1, "Cool", DateTime.Now.AddDays(8))),
                [DateTime.Now.AddDays(9)] = new List<AdvancedEventModel>(GenerateEvents(10, "Cool H", DateTime.Now.AddDays(9))),
                [DateTime.Now.AddDays(10)] = new List<AdvancedEventModel>(GenerateEvents(100, "Cool X", DateTime.Now.AddDays(10))),
                [DateTime.Now.AddDays(16)] = new List<AdvancedEventModel>(GenerateEvents(7, "Cool B", DateTime.Now.AddDays(16))),
                [DateTime.Now.AddDays(20)] = new List<AdvancedEventModel>(GenerateEvents(9, "Cool A", DateTime.Now.AddDays(20))),
                [DateTime.Now.AddDays(35)] = new List<AdvancedEventModel>(GenerateEvents(1, "Cool S", DateTime.Now.AddDays(35))),
                [DateTime.Now.AddDays(43)] = new List<AdvancedEventModel>(GenerateEvents(4, "Cool Q", DateTime.Now.AddDays(43))),
                [DateTime.Now.AddDays(46)] = new List<AdvancedEventModel>(GenerateEvents(12, "Cool ZZ", DateTime.Now.AddDays(46))),
                [DateTime.Now.AddDays(51)] = new List<AdvancedEventModel>(GenerateEvents(3, "Cool Y", DateTime.Now.AddDays(51))),
            };

            ShownDate = ShownDate.AddMonths(1);
        }

        public EventCollection Events { get; }

        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));

        public DateTime ShownDate
        {
            get => _shownDate;
            set => SetProperty(ref _shownDate, value);
        }

        public WeekLayout CalendarLayout
        {
            get => _calendarLayout;
            set => SetProperty(ref _calendarLayout, value);
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

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is AdvancedEventModel eventModel)
            {
                var title = $"Selected: {eventModel.Name}";
                var message = $"Starts: {eventModel.Starting:HH:mm}{Environment.NewLine}Details: {eventModel.Description}";
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
            }
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
    }
}
