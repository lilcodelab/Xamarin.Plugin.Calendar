﻿using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.Calendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthDaysView : ContentView
    {
        #region Bindable properties

        public static readonly BindableProperty MonthProperty =
          BindableProperty.Create(nameof(Month), typeof(int), typeof(MonthDaysView), DateTime.Now.Month, BindingMode.TwoWay);

        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty YearProperty =
          BindableProperty.Create(nameof(Year), typeof(int), typeof(MonthDaysView), DateTime.Now.Year, BindingMode.TwoWay);

        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(MonthDaysView), DateTime.Today, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(MonthDaysView), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(MonthDaysView), new EventCollection());

        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        public Color DaysTitleColor
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.White);

        public Color SelectedDayTextColor
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty DeselectedDayTextColorProperty =
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        public Color DeselectedDayTextColor
        {
            get => (Color)GetValue(DeselectedDayTextColorProperty);
            set => SetValue(DeselectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(MonthDaysView), Color.Silver);

        public Color OtherMonthDayColor
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#2196F3"));

        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        public static readonly BindableProperty EventIndicatorColorProperty =
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        public Color EventIndicatorColor
        {
            get => (Color)GetValue(EventIndicatorColorProperty);
            set => SetValue(EventIndicatorColorProperty, value);
        }

        #endregion

        private readonly List<DayView> _dayViews = new List<DayView>();
        private DayModel _selectedDay;
        private bool _animating;

        internal MonthDaysView()
        {
            InitializeComponent();

            InitializeDayViews();
            AssignDayViewModels();
            UpdateDaysColors();
            UpdateDayTitles();
            UpdateDays();
        }

        ~MonthDaysView()
        {
            DiposeDayViews();
        }

        #region PropertyChanged

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Month):
                case nameof(Year):
                case nameof(Events):
                    UpdateDays();
                    break;

                case nameof(SelectedDayTextColor):
                case nameof(OtherMonthDayColor):
                case nameof(DeselectedDayTextColor):
                case nameof(SelectedDayBackgroundColor):
                case nameof(EventIndicatorColor):
                    UpdateDaysColors();
                    break;

                case nameof(Culture):
                    UpdateDayTitles();
                    UpdateDays();
                    break;
            }
        }

        private void UpdateDayTitles()
        {
            var dayNumber = (int)Culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var dayLabel in daysTitleControl.Children.OfType<Label>())
            {
                dayLabel.Text = Culture.DateTimeFormat.DayNames[dayNumber].Substring(0, 3).ToUpper();
                dayNumber = (dayNumber + 1) % 7;
            }
        }

        private void UpdateDays()
        {
            if (Year == 0 || Month == 0 || Culture == null)
                return;

            Animate(() => daysControl.FadeTo(0, 50),
                    () => daysControl.FadeTo(1, 200),
                    () => LoadDays(),
                    () => Year * 100 + Month,
                    () => UpdateDays());
        }

        private void UpdateDaysColors()
        {
            foreach (var dayView in _dayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.SelectedTextColor = SelectedDayTextColor;
                dayModel.OtherMonthColor = OtherMonthDayColor;
                dayModel.DeselectedTextColor = DeselectedDayTextColor;
                dayModel.SelectedBackgroundColor = SelectedDayBackgroundColor;
                dayModel.EventIndicatorColor = EventIndicatorColor;
            }
        }

        private void OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DayModel.IsSelected)
                && sender is DayModel newSelected
                && newSelected.IsSelected)
            {
                if (newSelected.Date == SelectedDate)
                    return;

                SelectedDate = newSelected.Date;

                if (!newSelected.IsThisMonth)
                {
                    Year = newSelected.Date.Year;
                    Month = newSelected.Date.Month;
                    return;
                }

                if (_selectedDay != null)
                    _selectedDay.IsSelected = false;

                _selectedDay = newSelected;
            }
        }

        #endregion

        private void InitializeDayViews()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
                _dayViews.Add(dayView);
        }

        private void AssignDayViewModels()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
            {
                var dayModel = new DayModel();

                dayView.BindingContext = dayModel;
                dayModel.PropertyChanged += OnDayModelPropertyChanged;
            }
        }

        private void DiposeDayViews()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
            {
                (dayView.BindingContext as DayModel).PropertyChanged -= OnDayModelPropertyChanged;
                dayView.BindingContext = null;
            }
        }

        private void LoadDays()
        {
            DateTime monthStart = new DateTime(Year, Month, 1);
            var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

            if (addDays > 0)
                addDays -= 7;

            foreach (var dayView in _dayViews)
            {
                var currentDate = monthStart.AddDays(addDays++);

                // TODO: add indicator for current date (outline circle)
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate;
                dayModel.IsThisMonth = currentDate.Month == Month;
                dayModel.IsSelected = currentDate == SelectedDate.Date;
                dayModel.HasEvents = Events.ContainsKey(currentDate);

                if (dayModel.IsSelected)
                    _selectedDay = dayModel;
            }
        }

        private void Animate(
            Func<Task> animationIn,
            Func<Task> animationOut,
            Action afterFirstAnimation,
            Func<int> stateGetter,
            Action callAgain)
        {
            if (_animating)
                return;

            _animating = true;

            animationIn().ContinueWith(aIn =>
            {
                var prevState = stateGetter();
                afterFirstAnimation();

                animationOut().ContinueWith(aOut =>
                {
                    _animating = false;

                    if (stateGetter() != prevState)
                        callAgain();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}