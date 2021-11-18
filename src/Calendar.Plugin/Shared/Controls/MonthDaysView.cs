﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Controls.Interfaces;
using Xamarin.Plugin.Calendar.Controls.SelectionEngines;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Interfaces;
using Xamarin.Plugin.Calendar.Models;
using Xamarin.Plugin.Calendar.Shared.Controls.ViewLayoutEngines;
using Xamarin.Plugin.Calendar.Shared.Interfaces;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Internal class used by Xamarin.Plugin.Calendar
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthDaysView : ContentView
    {
        private Grid _daysControl;

        #region Bindable Properties

        /// <summary>
        /// Bindable property for ShownDate
        /// </summary>
        public static readonly BindableProperty ShownDateProperty =
          BindableProperty.Create(nameof(ShownDate), typeof(DateTime), typeof(MonthDaysView), DateTime.Today, BindingMode.TwoWay);

        /// <summary>
        /// Currently displayed month of selected year
        /// </summary>
        public DateTime ShownDate
        {
            get => (DateTime)GetValue(ShownDateProperty);
            set => SetValue(ShownDateProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDates
        /// </summary>
        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(List<DateTime>), typeof(MonthDaysView), new List<DateTime>(), BindingMode.TwoWay, propertyChanged: SelectedDatesChanged);

        private static void SelectedDatesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MonthDaysView control && (newValue is List<DateTime> || newValue is null) && !Equals(newValue, oldValue))
                control.UpdateDays();
        }

        /// <summary>
        /// Selected date in single date selection mode
        /// </summary>
        public List<DateTime> SelectedDates
        {
            get => (List<DateTime>)GetValue(SelectedDatesProperty);
            set => SetValue(SelectedDatesProperty, value);
        }

        /// <summary>
        /// Bindable property for Culture
        /// </summary>
        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(MonthDaysView), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        /// <summary>
        /// Culture info to properly format and name days
        /// </summary>
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        /// <summary>
        /// Bindable property for Events
        /// </summary>
        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(MonthDaysView), new EventCollection());

        /// <summary>
        /// Collection of all the events on the calendar
        /// </summary>
        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleColor
        /// </summary>
        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        /// <summary>
        /// Color of weekday titles
        /// </summary>
        public Color DaysTitleColor
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDayTextColor
        /// </summary>
        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.White);

        /// <summary>
        /// Color of selected dayView text
        /// </summary>
        public Color SelectedDayTextColor
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for DeselectedDayTextColor
        /// </summary>
        public static readonly BindableProperty DeselectedDayTextColorProperty =
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        /// <summary>
        /// Color of deselected day text
        /// </summary>
        public Color DeselectedDayTextColor
        {
            get => (Color)GetValue(DeselectedDayTextColorProperty);
            set => SetValue(DeselectedDayTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedTodayTextColor
        /// </summary>
        public static readonly BindableProperty SelectedTodayTextColorProperty =
            BindableProperty.Create(nameof(SelectedTodayTextColor), typeof(Color), typeof(MonthDaysView), Color.Transparent);

        /// <summary>
        /// Bindable property for SelectedTodayTextColor
        /// </summary>
        public Color SelectedTodayTextColor
        {
            get => (Color)GetValue(SelectedTodayTextColorProperty);
            set => SetValue(SelectedTodayTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for OtherMonthDayColor
        /// </summary>
        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(MonthDaysView), Color.Silver);

        /// <summary>
        /// Color of text for for days not belonging to the current month
        /// </summary>
        public Color OtherMonthDayColor
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        /// <summary>
        /// Bindable property for OtherMonthDayIsVisible
        /// </summary>
        public static readonly BindableProperty OtherMonthDayIsVisibleProperty =
          BindableProperty.Create(nameof(OtherMonthDayIsVisible), typeof(bool), typeof(MonthDaysView), true);

        /// <summary>
        /// Specifying if days from other months are visible on the current month view
        /// </summary>
        public bool OtherMonthDayIsVisible
        {
            get => (bool)GetValue(OtherMonthDayIsVisibleProperty);
            set => SetValue(OtherMonthDayIsVisibleProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDayBackgroundColor
        /// </summary>
        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#2196F3"));

        /// <summary>
        /// Background color of currently selected date
        /// </summary>
        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorColor
        /// </summary>
        public static readonly BindableProperty EventIndicatorTypeProperty =
          BindableProperty.Create(nameof(EventIndicatorType), typeof(EventIndicatorType), typeof(MonthDaysView), EventIndicatorType.BottomDot);

        /// <summary>
        /// Enum value specifying the way events are indicated on dates
        /// </summary>
        public EventIndicatorType EventIndicatorType
        {
            get => (EventIndicatorType)GetValue(EventIndicatorTypeProperty);
            set => SetValue(EventIndicatorTypeProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorColor
        /// </summary>
        public static readonly BindableProperty EventIndicatorColorProperty =
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        /// <summary>
        /// Color of event indicator on dates
        /// </summary>
        public Color EventIndicatorColor
        {
            get => (Color)GetValue(EventIndicatorColorProperty);
            set => SetValue(EventIndicatorColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorSelectedColor
        /// </summary>
        public static readonly BindableProperty EventIndicatorSelectedColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        /// <summary>
        /// Color of event indicator on selected dates
        /// </summary>
        public Color EventIndicatorSelectedColor
        {
            get => (Color)GetValue(EventIndicatorSelectedColorProperty);
            set => SetValue(EventIndicatorSelectedColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorTextColor
        /// </summary>
        public static readonly BindableProperty EventIndicatorTextColorProperty =
         BindableProperty.Create(nameof(EventIndicatorTextColor), typeof(Color?), typeof(Calendar), Color.Default);

        /// <summary>
        /// Color of event indicator text
        /// </summary>
        public Color EventIndicatorTextColor
        {
            get => (Color)GetValue(EventIndicatorTextColorProperty);
            set => SetValue(EventIndicatorTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorSelectedTextColor
        /// </summary>
        public static readonly BindableProperty EventIndicatorSelectedTextColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedTextColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Color of event indicator text on selected dates
        /// </summary>
        public Color EventIndicatorSelectedTextColor
        {
            get => (Color)GetValue(EventIndicatorSelectedTextColorProperty);
            set => SetValue(EventIndicatorSelectedTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for TodayOutlineColor
        /// </summary>
        public static readonly BindableProperty TodayOutlineColorProperty =
          BindableProperty.Create(nameof(TodayOutlineColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        /// <summary>
        /// Color of today date's outline
        /// </summary>
        public Color TodayOutlineColor
        {
            get => (Color)GetValue(TodayOutlineColorProperty);
            set => SetValue(TodayOutlineColorProperty, value);
        }

        /// <summary>
        /// Bindable property for TodayTextColor
        /// </summary>
        public static readonly BindableProperty TodayTextColorProperty =
            BindableProperty.Create(nameof(TodayTextColor), typeof(Color), typeof(MonthDaysView), Color.Transparent);

        /// <summary>
        /// Color of today date's text
        /// </summary>
        public Color TodayTextColor
        {
            get => (Color)GetValue(TodayTextColorProperty);
            set => SetValue(TodayTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for TodayFillColor
        /// </summary>
        public static readonly BindableProperty TodayFillColorProperty =
          BindableProperty.Create(nameof(TodayFillColor), typeof(Color), typeof(MonthDaysView), Color.Transparent);

        /// <summary>
        /// Color of today date's fill
        /// </summary>
        public Color TodayFillColor
        {
            get => (Color)GetValue(TodayFillColorProperty);
            set => SetValue(TodayFillColorProperty, value);
        }

        /// <summary>
        /// Bindable property for DayViewSize
        /// </summary>
        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(MonthDaysView), 40.0);

        /// <summary>
        /// Size of all individual dates
        /// </summary>
        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        /// <summary>
        /// Bindable property for DayViewCornerRadius
        /// </summary>
        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(MonthDaysView), 20f);

        /// <summary>
        /// Corner radius of individual dates
        /// </summary>
        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleHeight
        /// </summary>
        public static readonly BindableProperty DaysTitleHeightProperty =
          BindableProperty.Create(nameof(DaysTitleHeight), typeof(double), typeof(MonthDaysView), 30.0);

        /// <summary>
        /// Height of the weekday names container
        /// </summary>
        public double DaysTitleHeight
        {
            get => (double)GetValue(DaysTitleHeightProperty);
            set => SetValue(DaysTitleHeightProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleMaximumLength
        /// </summary>
        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
        BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(MonthDaysView), DaysTitleMaxLength.ThreeChars);

        /// <summary>
        /// Maximum character length of weekday titles
        /// </summary>
        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysLabelStyle
        /// </summary>
        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(MonthDaysView), Device.Styles.BodyStyle);

        /// <summary>
        /// Style of weekday labels
        /// </summary>
        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleLabelStyle
        /// </summary>
        public static readonly BindableProperty DaysTitleLabelStyleProperty =
          BindableProperty.Create(nameof(DaysTitleLabelStyle), typeof(Style), typeof(MonthDaysView), null);

        /// <summary>
        /// ???
        /// </summary>
        public Style DaysTitleLabelStyle
        {
            get => (Style)GetValue(DaysTitleLabelStyleProperty);
            set => SetValue(DaysTitleLabelStyleProperty, value);
        }

        /// <summary>
        /// Bindable property for DayTapped
        /// </summary>
        public static readonly BindableProperty DayTappedCommandProperty =
            BindableProperty.Create(nameof(DayTappedCommand), typeof(ICommand), typeof(MonthDaysView), null);

        /// <summary>
        /// Action to run after a day has been tapped.
        /// </summary>
        public ICommand DayTappedCommand
        {
            get => (ICommand)GetValue(DayTappedCommandProperty);
            set => SetValue(DayTappedCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for MinimumDate
        /// </summary>
        public static readonly BindableProperty MinimumDateProperty =
          BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(MonthDaysView), DateTime.MinValue);

        /// <summary>
        /// Minimum date which can be selected
        /// </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary>
        /// Bindable property for MaximumDate
        /// </summary>
        public static readonly BindableProperty MaximumDateProperty =
          BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(MonthDaysView), DateTime.MaxValue);

        /// <summary>
        /// Maximum date which can be selected
        /// </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary>
        /// Bindable property for DisabledDayColor
        /// </summary>
        public static readonly BindableProperty DisabledDayColorProperty =
          BindableProperty.Create(nameof(DisabledDayColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#ECECEC"));

        /// <summary>
        /// Color for days which are out of MinimumDate - MaximumDate range
        /// </summary>
        public Color DisabledDayColor
        {
            get => (Color)GetValue(DisabledDayColorProperty);
            set => SetValue(DisabledDayColorProperty, value);
        }

        /// <summary>
        /// Bindable property for AnimateCalendar
        /// </summary>
        public static readonly BindableProperty AnimateCalendarProperty =
            BindableProperty.Create(nameof(AnimateCalendar), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies if the calendar should animate or not
        /// </summary>
        public bool AnimateCalendar
        {
            get => (bool)GetValue(AnimateCalendarProperty);
            set => SetValue(AnimateCalendarProperty, value);
        }

        /// <summary>
        /// Bindable property for WeekLayout
        /// </summary>
        public static readonly BindableProperty CalendarLayoutProperty =
            BindableProperty.Create(nameof(CalendarLayout), typeof(WeekLayout), typeof(Calendar), WeekLayout.Month);

        /// <summary>
        /// Sets the layout of the calendar
        /// </summary>
        public WeekLayout CalendarLayout
        {
            get => (WeekLayout)GetValue(CalendarLayoutProperty);
            set => SetValue(CalendarLayoutProperty, value);
        }

        #endregion

        /// <summary>
        /// Current implementation of selection engine
        /// </summary>
        internal ISelectionEngine CurrentSelectionEngine { get; set; } = new SingleSelectionEngine();

        internal IViewLayoutEngine CurrentViewLayoutEngine { get; set; } = new MonthViewEngine(CultureInfo.InvariantCulture);

        private readonly Dictionary<string, bool> _propertyChangedNotificationSupressions = new();
        private readonly List<DayView> _dayViews = new();
        private DateTime _lastAnimationTime;
        private bool _animating;

        internal MonthDaysView()
        {
            RenderLayout();
        }

        /// <summary>
        /// Destructor for optimization
        /// </summary>
        ~MonthDaysView() => DiposeDayViews();

        #region PropertyChanged

        /// <summary>
        /// Method that is called when a bound property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the bound property that changed.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyChangedNotificationSupressions.TryGetValue(propertyName, out bool isSuppressed)
                && isSuppressed)
                return;

            switch (propertyName)
            {
                case nameof(SelectedDates):
                    CurrentSelectionEngine.UpdateDateSelection(SelectedDates);
                    break;

                case nameof(Events):
                case nameof(ShownDate):
                case nameof(MinimumDate):
                case nameof(MaximumDate):
                case nameof(OtherMonthDayIsVisible):
                    UpdateAndAnimateDays(AnimateCalendar);
                    break;

                case nameof(TodayTextColor):
                case nameof(SelectedDayTextColor):
                case nameof(SelectedTodayTextColor):
                case nameof(OtherMonthDayColor):
                case nameof(DeselectedDayTextColor):
                case nameof(SelectedDayBackgroundColor):
                case nameof(EventIndicatorColor):
                case nameof(EventIndicatorSelectedColor):
                case nameof(EventIndicatorTextColor):
                case nameof(EventIndicatorSelectedTextColor):
                case nameof(EventIndicatorType):
                case nameof(TodayOutlineColor):
                case nameof(TodayFillColor):
                case nameof(DisabledDayColor):
                    UpdateDaysColors();
                    break;

                case nameof(Culture):
                    RenderLayout();
                    UpdateAndAnimateDays(AnimateCalendar);
                    break;

                case nameof(DaysTitleMaximumLength):
                    UpdateDayTitles();
                    break;

                case nameof(CalendarLayout):
                    RenderLayout();
                    break;
            }
        }

        private void OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DayModel.IsSelected) || sender is not DayModel newSelected ||
                (_propertyChangedNotificationSupressions.TryGetValue(e.PropertyName, out bool isSuppressed) && isSuppressed))
                return;

            SelectedDates = CurrentSelectionEngine.PerformDateSelection(newSelected.Date);
        }

        private void UpdateDayTitles()
        {
            var dayNumber = (int)Culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var dayLabel in _daysControl.Children.OfType<Label>())
            {
                var abberivatedDayName = Culture.DateTimeFormat.AbbreviatedDayNames[dayNumber];
                dayLabel.Text = abberivatedDayName.ToUpper().Substring(0, (int)DaysTitleMaximumLength > abberivatedDayName.Length ? abberivatedDayName.Length : (int)DaysTitleMaximumLength);
                dayNumber = (dayNumber + 1) % 7;
            }
        }

        internal void UpdateAndAnimateDays(bool animate)
        {
            if (Culture == null)
                return;

            Animate(() => _daysControl.FadeTo(animate ? 0 : 1, 50),
                    () => _daysControl.FadeTo(1, 200),
                    () => UpdateDays(),
                    _lastAnimationTime = DateTime.UtcNow,
                    () => UpdateAndAnimateDays(false));//send false to prevent flashing if several property bindings are changed
        }

        private void UpdateDays()
        {
            var firstDate = CurrentViewLayoutEngine.GetFirstDate(ShownDate);

            int addDays = 0;
            foreach (var dayView in _dayViews)
            {
                var currentDate = firstDate.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate.Date;
                dayModel.DayTappedCommand = DayTappedCommand;
                dayModel.EventIndicatorType = EventIndicatorType;
                dayModel.DayViewSize = DayViewSize;
                dayModel.DayViewCornerRadius = DayViewCornerRadius;
                dayModel.DaysLabelStyle = DaysLabelStyle;
                dayModel.IsThisMonth = (CalendarLayout != WeekLayout.Month) || currentDate.Month == ShownDate.Month;
                dayModel.OtherMonthIsVisible = (CalendarLayout != WeekLayout.Month) || OtherMonthDayIsVisible;
                dayModel.HasEvents = Events.ContainsKey(currentDate);
                dayModel.IsDisabled = currentDate < MinimumDate || currentDate > MaximumDate;

                ChangePropertySilently(nameof(dayModel.IsSelected), () => dayModel.IsSelected = CurrentSelectionEngine.IsDateSelected(dayModel.Date));
                AssignIndicatorColors(ref dayModel);
            }
        }

        private void UpdateDaysColors()
        {
            foreach (var dayView in _dayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.DeselectedTextColor = DeselectedDayTextColor;
                dayModel.TodayTextColor = TodayTextColor;
                dayModel.SelectedTextColor = SelectedDayTextColor;
                dayModel.SelectedTodayTextColor = SelectedTodayTextColor;
                dayModel.OtherMonthColor = OtherMonthDayColor;
                dayModel.SelectedBackgroundColor = SelectedDayBackgroundColor;
                dayModel.TodayOutlineColor = TodayOutlineColor;
                dayModel.TodayFillColor = TodayFillColor;
                dayModel.DisabledColor = DisabledDayColor;

                AssignIndicatorColors(ref dayModel);
            }
        }

        #endregion

        private void RenderLayout()
        {
            CurrentViewLayoutEngine = CalendarLayout switch
            {
                WeekLayout.Week => new WeekViewEngine(Culture, 1),
                WeekLayout.TwoWeek => new WeekViewEngine(Culture, 2),
                _ => new MonthViewEngine(Culture),
            };

            _daysControl = CurrentViewLayoutEngine.GenerateLayout(
                _dayViews,
                DaysTitleHeight,
                DaysTitleColor,
                DaysTitleLabelStyle,
                DayViewSize,
                DayTappedCommand,
                OnDayModelPropertyChanged);

            UpdateDaysColors();
            UpdateDayTitles();

            Content = _daysControl;
        }

        private void DiposeDayViews()
        {
            foreach (var dayView in _daysControl.Children.OfType<DayView>())
            {
                (dayView.BindingContext as DayModel).PropertyChanged -= OnDayModelPropertyChanged;
                dayView.BindingContext = null;
            }
        }

        private void Animate(
            Func<Task> animationIn,
            Func<Task> animationOut,
            Action afterFirstAnimation,
            DateTime animationTime,
            Action callAgain)
        {
            if (_animating)
                return;

            _animating = true;

            animationIn().ContinueWith(aIn =>
            {
                afterFirstAnimation();

                animationOut().ContinueWith(aOut =>
                {
                    _animating = false;

                    if (animationTime != _lastAnimationTime)
                        callAgain();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        internal void ChangePropertySilently(string propertyName, Action propertyChangeAction)
        {
            _propertyChangedNotificationSupressions[propertyName] = true;
            propertyChangeAction();
            _propertyChangedNotificationSupressions[propertyName] = false;
        }

        internal void AssignIndicatorColors(ref DayModel dayModel)
        {
            Color? eventIndicatorColor = EventIndicatorColor;
            Color? eventIndicatorSelectedColor = EventIndicatorSelectedColor;
            Color? eventIndicatorTextColor = EventIndicatorTextColor;
            Color? eventIndicatorSelectedTextColor = EventIndicatorSelectedTextColor;

            if (Events.TryGetValue(dayModel.Date, out var dayEventCollection) && dayEventCollection is IPersonalizableDayEvent personalizableDay)
            {
                eventIndicatorColor = personalizableDay?.EventIndicatorColor;
                eventIndicatorSelectedColor = personalizableDay?.EventIndicatorSelectedColor ?? personalizableDay?.EventIndicatorColor;
                eventIndicatorTextColor = personalizableDay?.EventIndicatorTextColor;
                eventIndicatorSelectedTextColor = personalizableDay?.EventIndicatorSelectedTextColor ?? personalizableDay?.EventIndicatorTextColor;
            }

            dayModel.EventIndicatorColor = eventIndicatorColor ?? EventIndicatorColor;
            dayModel.EventIndicatorSelectedColor = eventIndicatorSelectedColor ?? EventIndicatorSelectedColor;
            dayModel.EventIndicatorTextColor = eventIndicatorTextColor ?? EventIndicatorTextColor;
            dayModel.EventIndicatorSelectedTextColor = eventIndicatorSelectedTextColor ?? EventIndicatorSelectedTextColor;
        }
    }
}
