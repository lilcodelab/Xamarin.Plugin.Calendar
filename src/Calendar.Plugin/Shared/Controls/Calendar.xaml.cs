﻿using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.Calendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentView
    {
        #region Bindable properties

        public static readonly BindableProperty MonthProperty =
          BindableProperty.Create(nameof(Month), typeof(int), typeof(Calendar), DateTime.Now.Month, BindingMode.TwoWay);

        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty YearProperty =
          BindableProperty.Create(nameof(Year), typeof(int), typeof(Calendar), DateTime.Now.Year, BindingMode.TwoWay);

        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(Calendar), DateTime.Today, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(Calendar), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(Calendar), new EventCollection());

        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        public static readonly BindableProperty SelectedDayEventsProperty =
          BindableProperty.Create(nameof(SelectedDayEvents), typeof(ICollection), typeof(Calendar), new List<object>());

        public ICollection SelectedDayEvents
        {
            get => (ICollection)GetValue(SelectedDayEventsProperty);
            set => SetValue(SelectedDayEventsProperty, value);
        }

        public static readonly BindableProperty EventTemplateProperty =
          BindableProperty.Create(nameof(EventTemplate), typeof(DataTemplate), typeof(Calendar), null);

        public DataTemplate EventTemplate
        {
            get => (DataTemplate)GetValue(EventTemplateProperty);
            set => SetValue(EventTemplateProperty, value);
        }

        public static readonly BindableProperty MonthLabelColorProperty =
          BindableProperty.Create(nameof(MonthLabelColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        public Color MonthLabelColor
        {
            get => (Color)GetValue(MonthLabelColorProperty);
            set => SetValue(MonthLabelColorProperty, value);
        }

        public static readonly BindableProperty YearLabelColorProperty =
          BindableProperty.Create(nameof(YearLabelColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        public Color YearLabelColor
        {
            get => (Color)GetValue(YearLabelColorProperty);
            set => SetValue(YearLabelColorProperty, value);
        }

        public static readonly BindableProperty SelectedDateColorProperty =
          BindableProperty.Create(nameof(SelectedDateColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        public Color SelectedDateColor
        {
            get => (Color)GetValue(SelectedDateColorProperty);
            set => SetValue(SelectedDateColorProperty, value);
        }

        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(Calendar), Color.Default);

        public Color DaysTitleColor
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(Calendar), Color.White);

        public Color SelectedDayTextColor
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty DeselectedDayTextColorProperty =
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(Calendar), Color.Default);

        public Color DeselectedDayTextColor
        {
            get => (Color)GetValue(DeselectedDayTextColorProperty);
            set => SetValue(DeselectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(Calendar), Color.Silver);

        public Color OtherMonthDayColor
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        public static readonly BindableProperty EventIndicatorColorProperty =
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(Calendar), Color.FromHex("#FF4081"));

        public Color EventIndicatorColor
        {
            get => (Color)GetValue(EventIndicatorColorProperty);
            set => SetValue(EventIndicatorColorProperty, value);
        }

        public static readonly BindableProperty ArrowsColorProperty =
          BindableProperty.Create(nameof(ArrowsColor), typeof(Color), typeof(Calendar), Color.Default);

        public Color ArrowsColor
        {
            get => (Color)GetValue(ArrowsColorProperty);
            set => SetValue(ArrowsColorProperty, value);
        }

        #endregion

        private const uint MonthsAnimationRate = 16;
        private const int MonthsAnimationDuration = 200;
        private const string MonthsAnimationId = nameof(MonthsAnimationId);
        private readonly Animation _monthsAnimateIn;
        private readonly Animation _monthsAnimateOut;
        private bool _monthViewAnimating;
        private bool _monthViewShown;

        public Calendar()
        {
            InitializeComponent();
            UpdateSelectedDateLabel();
            UpdateMonthLabel();
            UpdateEvents();

            _monthsAnimateIn = new Animation(AnimateMonths, 1, 0);
            _monthsAnimateOut = new Animation(AnimateMonths, 0, 1);
        }

        #region PropertyChanged

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Month):
                    UpdateMonthLabel();
                    break;

                case nameof(Events):
                    UpdateEvents();
                    break;

                case nameof(SelectedDate):
                    UpdateSelectedDateLabel();
                    UpdateEvents();
                    break;

                case nameof(Culture):
                    if (Month > 0)
                        UpdateMonthLabel();

                    UpdateSelectedDateLabel();
                    break;
            }
        }

        private void UpdateEvents()
        {
            if (Events.TryGetValue(SelectedDate, out var eventList))
                SelectedDayEvents = eventList;
            else
                SelectedDayEvents = null;

            eventsScrollView.ScrollToAsync(0, 0, false);
        }

        private void UpdateMonthLabel()
        {
            monthLabel.Text = Culture.DateTimeFormat.MonthNames[Month - 1].Capitalize();
        }

        private void UpdateSelectedDateLabel()
        {
            selectedDateLabel.Text = SelectedDate.ToString("d MMM yyyy", Culture);
        }

        #endregion

        #region Event Handlers

        private void PrevMonthClicked(object sender, EventArgs e)
        {
            if (Month - 1 == 0)
            {
                Month = 12;
                Year--;
            }
            else
                Month--;
        }

        private void NextMonthClicked(object sender, EventArgs e)
        {
            if (Month + 1 == 13)
            {
                Month = 1;
                Year++;
            }
            else
                Month++;
        }

        private void PrevYearClicked(object sender, EventArgs e)
        {
            Year--;
        }

        private void NextYearClicked(object sender, EventArgs e)
        {
            Year++;
        }

        private void OnMonthsShowHideTapped(object sender, EventArgs e)
        {
            _monthViewShown = !_monthViewShown;
            showHideLabel.Text = _monthViewShown ? "↓" : "↑";

            if (_monthViewAnimating)
                return;

            _monthViewAnimating = true;

            var animation = _monthViewShown ? _monthsAnimateIn : _monthsAnimateOut;
            var prevState = _monthViewShown;

            animation.Commit(this, MonthsAnimationId, MonthsAnimationRate, MonthsAnimationDuration, finished: (value, cancelled) =>
            {
                _monthViewAnimating = false;

                if (prevState != _monthViewShown)
                    OnMonthsShowHideTapped(null, null);
            });
        }

        #endregion

        private void AnimateMonths(double currentValue)
        {
            monthsRow.Height = new GridLength(monthsView.Height * currentValue);
            monthsView.TranslationY = monthsView.Height * (currentValue - 1);
            monthsView.Opacity = currentValue * currentValue * currentValue;
        }

    }
}