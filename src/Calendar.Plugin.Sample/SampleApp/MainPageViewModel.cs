using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SampleApp
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private CultureInfo _culture = CultureInfo.InvariantCulture;

        public MainPageViewModel()
        {
            Culture = CultureInfo.CreateSpecificCulture("en-US");

            // testing all kinds of adding events
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-3)] = new List<EventModel>
                {
                    new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                    new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                    new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!" },
                    new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!" },
                    new EventModel { Name = "Cool event5", Description = "This is Cool event5's description!" },
                }
            };

            Events.Add(DateTime.Now.AddDays(-2), new List<EventModel>
            {
                new EventModel { Name = "Cool event1", Description = "This is Cool event1's description!" },
                new EventModel { Name = "Cool event2", Description = "This is Cool event2's description!" },
                new EventModel { Name = "Cool event3", Description = "This is Cool event3's description!" },
                new EventModel { Name = "Cool event4", Description = "This is Cool event4's description!" },
                new EventModel { Name = "Cool event5", Description = "This is Cool event5's description!" },
            });

            Events[DateTime.Now] = new List<EventModel>
            {
                new EventModel { Name = "Boring event1", Description = "This is Boring event1's description!" },
                new EventModel { Name = "Boring event2", Description = "This is Boring event2's description!" },
            };

            Task.Delay(5000).ContinueWith(_ =>
            {
                Events[DateTime.Now] = new List<EventModel>
                {
                    new EventModel { Name = "Boring event1", Description = "This is Boring event1's description!" },
                    new EventModel { Name = "Boring event2", Description = "This is Boring event2's description!" },
                    new EventModel { Name = "Boring event3", Description = "This is Boring event3's description!" },
                    new EventModel { Name = "Boring event4", Description = "This is Boring event4's description!" },
                    new EventModel { Name = "Boring event5", Description = "This is Boring event5's description!" },
                    new EventModel { Name = "Boring event6", Description = "This is Boring event6's description!" },
                    new EventModel { Name = "Boring event7", Description = "This is Boring event7's description!" },
                    new EventModel { Name = "Boring event8", Description = "This is Boring event8's description!" },
                    new EventModel { Name = "Boring event9", Description = "This is Boring event9's description!" },
                    new EventModel { Name = "Boring event10", Description = "This is Boring event10's description!" },
                };

                Events.Add(DateTime.Now.AddDays(3), new List<EventModel>
                {
                    new EventModel { Name = "Cool event6", Description = "This is Cool event6's description!" },
                    new EventModel { Name = "Cool event7", Description = "This is Cool event7's description!" },
                    new EventModel { Name = "Cool event8", Description = "This is Cool event8's description!" },
                    new EventModel { Name = "Cool event9", Description = "This is Cool event9's description!" },
                    new EventModel { Name = "Cool event10", Description = "This is Cool event10's description!" },
                });

                Events[DateTime.Now.AddDays(10)] = new List<EventModel>
                {
                    new EventModel { Name = "Boring event11", Description = "This is Boring event11's description!" },
                    new EventModel { Name = "Boring event12", Description = "This is Boring event12's description!" },
                    new EventModel { Name = "Boring event13", Description = "This is Boring event13's description!" },
                    new EventModel { Name = "Boring event14", Description = "This is Boring event14's description!" },
                    new EventModel { Name = "Boring event15", Description = "This is Boring event15's description!" },
                    new EventModel { Name = "Boring event16", Description = "This is Boring event16's description!" },
                    new EventModel { Name = "Boring event17", Description = "This is Boring event17's description!" },
                    new EventModel { Name = "Boring event18", Description = "This is Boring event18's description!" },
                    new EventModel { Name = "Boring event19", Description = "This is Boring event19's description!" },
                };

                Events.Add(DateTime.Now.AddDays(15), new List<EventModel>
                {
                    new EventModel { Name = "Cool event11", Description = "This is Cool event11's description!" },
                    new EventModel { Name = "Cool event12", Description = "This is Cool event12's description!" },
                    new EventModel { Name = "Cool event13", Description = "This is Cool event13's description!" },
                    new EventModel { Name = "Cool event14", Description = "This is Cool event14's description!" },
                    new EventModel { Name = "Cool event15", Description = "This is Cool event15's description!" },
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public EventCollection Events { get; }
        public int Month { get; set; } = DateTime.Now.Month;
        public int Year { get; set; } = DateTime.Now.Year;

        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
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
