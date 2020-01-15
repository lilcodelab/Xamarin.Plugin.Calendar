using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public ICommand DayTappedCommand => new Command<DateTime>(DayTapped);

        public MainPageViewModel()
        {
            Culture = CultureInfo.CreateSpecificCulture("en-US");

            // testing all kinds of adding events
            // when initializing collection
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-3)] = new List<EventModel>(GenerateEvents(10, "Cool")),
            };

            // with add method
            Events.Add(DateTime.Now.AddDays(-1), new List<EventModel>(GenerateEvents(5, "Cool")));
            
            // with indexer
            Events[DateTime.Now] = new List<EventModel>(GenerateEvents(2, "Boring"));

            Task.Delay(5000).ContinueWith(_ =>
            {
                // indexer - update later
                Events[DateTime.Now] = new ObservableCollection<EventModel>(GenerateEvents(10, "Cool"));

                // add later
                Events.Add(DateTime.Now.AddDays(3), new List<EventModel>(GenerateEvents(5, "Cool")));

                // indexer later
                Events[DateTime.Now.AddDays(10)] = new List<EventModel>(GenerateEvents(10, "Boring"));

                // add later
                Events.Add(DateTime.Now.AddDays(15), new List<EventModel>(GenerateEvents(10, "Cool")));

                Task.Delay(3000).ContinueWith(t =>
                {
                    // get observable collection later
                    var todayEvents = Events[DateTime.Now] as ObservableCollection<EventModel>;

                    // insert/add items to observable collection
                    todayEvents.Insert(0, new EventModel { Name = "Cool event insert", Description = "This is Cool event's description!" });
                    todayEvents.Add(new EventModel { Name = "Cool event add", Description = "This is Cool event's description!" });
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());

            Task.Delay(5000).ContinueWith(_ =>
            {
                SelectedDate = DateTime.Today.AddDays(35).Date;
            });
        }

        private IEnumerable<EventModel> GenerateEvents(int count, string name)
        {
            return Enumerable.Range(1, count).Select(x => new EventModel
            {
                Name = $"{name} event{x}",
                Description = $"This is {name} event{x}'s description!"
            });
        }

        public EventCollection Events { get; }
        public int Month { get; set; } = DateTime.Now.Month;
        public int Year { get; set; } = DateTime.Now.Year;

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        private static void DayTapped(DateTime date)
        {
            Console.WriteLine($"Received tap event from date: {date}");
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<TData>(ref TData storage, TData value, [CallerMemberName] string propertyName = "")
        {
            if (storage.Equals(value))
                return;

            storage = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
