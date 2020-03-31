using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Plugin.Calendar.Models;

namespace SampleApp
{
    public class MyDemoViewModel : INotifyPropertyChanged
    {

        public MyDemoViewModel()
        {
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                List<DateTime> list = new List<DateTime>();

                list.Add(DateTime.Now.AddDays(-1));
                list.Add(DateTime.Now.AddDays(-2));
                list.Add(DateTime.Now.AddDays(1).AddSeconds(10));
                MarkDates = list;
            });


            //MarkDates.Add(DateTime.Now.AddDays(-1));
            //MarkDates.Add(DateTime.Now.AddDays(-2));
            //MarkDates.Add(DateTime.Now.AddDays(1).AddSeconds(10));
        }


        public EventCollection Events { get; }
        public int Month { get; set; } = DateTime.Now.Month;
        public int Year { get; set; } = DateTime.Now.Year;

        private DateTime _selectedDate = DateTime.Today;

        private List<DateTime> _markDates = new List<DateTime>();
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public List<DateTime> MarkDates
        {
            get => _markDates;
            set => SetProperty(ref _markDates, value);
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
