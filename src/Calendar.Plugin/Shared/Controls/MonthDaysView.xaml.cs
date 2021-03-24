using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;
using System.Windows.Input;
using Xamarin.Plugin.Calendar.Interfaces;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Internal class used by Xamarin.Plugin.Calendar
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthDaysView : ContentView
    {
        #region Bindable properties

        public static readonly BindableProperty DisplayedMonthYearProperty =
          BindableProperty.Create(nameof(DisplayedMonthYear), typeof(DateTime), typeof(MonthDaysView), DateTime.Today, BindingMode.TwoWay);

        public DateTime DisplayedMonthYear
        {
            get => (DateTime)GetValue(DisplayedMonthYearProperty);
            set => SetValue(DisplayedMonthYearProperty, value);
        }

        /// <summary> Bindable property for SelectedDate </summary>
        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(MonthDaysView), DateTime.Today, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        /// <summary> Bindable property for Culture </summary>
        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(MonthDaysView), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        /// <summary> Bindable property for Events </summary>
        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(MonthDaysView), new EventCollection());

        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        /// <summary> Bindable property for DaysTitleColor </summary>
        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        public Color DaysTitleColor
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        /// <summary> Bindable property for SelectedDayTextColor </summary>
        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.White);

        public Color SelectedDayTextColor
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        /// <summary> Bindable property for DeselectedDayTextColor </summary>
        public static readonly BindableProperty DeselectedDayTextColorProperty =
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(MonthDaysView), Color.Default);

        public Color DeselectedDayTextColor
        {
            get => (Color)GetValue(DeselectedDayTextColorProperty);
            set => SetValue(DeselectedDayTextColorProperty, value);
        }

        /// <summary> Bindable property for OtherMonthDayColor </summary>
        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(MonthDaysView), Color.Silver);

        public Color OtherMonthDayColor
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        /// <summary> Bindable property for OtherMonthDayIsVisible </summary>
        public static readonly BindableProperty OtherMonthDayIsVisibleProperty =
          BindableProperty.Create(nameof(OtherMonthDayIsVisible), typeof(bool), typeof(MonthDaysView), true);

        public bool OtherMonthDayIsVisible
        {
            get => (bool)GetValue(OtherMonthDayIsVisibleProperty);
            set => SetValue(OtherMonthDayIsVisibleProperty, value);
        }

        /// <summary> Bindable property for SelectedDayBackgroundColor </summary>
        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#2196F3"));

        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        /// <summary> Bindable property for EventIndicatorColor </summary>
        public static readonly BindableProperty EventIndicatorTypeProperty =
          BindableProperty.Create(nameof(EventIndicatorType), typeof(EventIndicatorType), typeof(MonthDaysView), EventIndicatorType.BottomDot);

        public EventIndicatorType EventIndicatorType
        {
            get => (EventIndicatorType)GetValue(EventIndicatorTypeProperty);
            set => SetValue(EventIndicatorTypeProperty, value);
        }

        /// <summary> Bindable property for EventIndicatorColor </summary>
        public static readonly BindableProperty EventIndicatorColorProperty =
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        public Color EventIndicatorColor
        {
            get => (Color)GetValue(EventIndicatorColorProperty);
            set => SetValue(EventIndicatorColorProperty, value);
        }

        /// <summary> Bindable property for EventIndicatorSelectedColor </summary>
        public static readonly BindableProperty EventIndicatorSelectedColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        public Color EventIndicatorSelectedColor
        {
            get => (Color)GetValue(EventIndicatorSelectedColorProperty);
            set => SetValue(EventIndicatorSelectedColorProperty, value);
        }

        /// <summary> Bindable property for EventIndicatorTextColor </summary>
        public static readonly BindableProperty EventIndicatorTextColorProperty =
         BindableProperty.Create(nameof(EventIndicatorTextColor), typeof(Color?), typeof(Calendar), Color.Default);

        public Color EventIndicatorTextColor
        {
            get => (Color)GetValue(EventIndicatorTextColorProperty);
            set => SetValue(EventIndicatorTextColorProperty, value);
        }

        /// <summary> Bindable property for EventIndicatorSelectedTextColor </summary>
        public static readonly BindableProperty EventIndicatorSelectedTextColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedTextColor), typeof(Color), typeof(Calendar), Color.Default);

        public Color EventIndicatorSelectedTextColor
        {
            get => (Color)GetValue(EventIndicatorSelectedTextColorProperty);
            set => SetValue(EventIndicatorSelectedTextColorProperty, value);
        }

        /// <summary> Bindable property for TodayOutlineColor </summary>
        public static readonly BindableProperty TodayOutlineColorProperty =
          BindableProperty.Create(nameof(TodayOutlineColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#FF4081"));

        public Color TodayOutlineColor
        {
            get => (Color)GetValue(TodayOutlineColorProperty);
            set => SetValue(TodayOutlineColorProperty, value);
        }

        /// <summary> Bindable property for TodayFillColor </summary>
        public static readonly BindableProperty TodayFillColorProperty =
          BindableProperty.Create(nameof(TodayFillColor), typeof(Color), typeof(MonthDaysView), Color.Transparent);

        public Color TodayFillColor
        {
            get => (Color)GetValue(TodayFillColorProperty);
            set => SetValue(TodayFillColorProperty, value);
        }

        /// <summary> Bindable property for DayViewSize </summary>
        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(MonthDaysView), 40.0);

        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        /// <summary> Bindable property for DayViewCornerRadius </summary>
        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(MonthDaysView), 20f);

        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        /// <summary> Bindable property for DaysTitleHeight </summary>
        public static readonly BindableProperty DaysTitleHeightProperty =
          BindableProperty.Create(nameof(DaysTitleHeight), typeof(double), typeof(MonthDaysView), 30.0);

        public double DaysTitleHeight
        {
            get => (double)GetValue(DaysTitleHeightProperty);
            set => SetValue(DaysTitleHeightProperty, value);
        }

        /// <summary> Bindable property for DaysTitleMaximumLength </summary>
        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
        BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(MonthDaysView), DaysTitleMaxLength.ThreeChars);

        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }

        /// <summary> Bindable property for DaysLabelStyle </summary>
        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(MonthDaysView), Device.Styles.BodyStyle);

        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        /// <summary> Bindable property for DaysTitleLabelStyle </summary>
        public static readonly BindableProperty DaysTitleLabelStyleProperty =
          BindableProperty.Create(nameof(DaysTitleLabelStyle), typeof(Style), typeof(MonthDaysView), null);

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

        /// <summary> Bindable property for MinimumDate </summary>
        public static readonly BindableProperty MinimumDateProperty =
          BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(MonthDaysView), DateTime.MinValue);

        /// <summary> Minimum date which can be selected </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary> Bindable property for MaximumDate </summary>
        public static readonly BindableProperty MaximumDateProperty =
          BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(MonthDaysView), DateTime.MaxValue);

        /// <summary> Maximum date which can be selected </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary> Bindable property for DisabledDayColor </summary>
        public static readonly BindableProperty DisabledDayColorProperty =
          BindableProperty.Create(nameof(DisabledDayColor), typeof(Color), typeof(MonthDaysView), Color.FromHex("#ECECEC"));

        /// <summary> Color for days which are out of MinimumDate - MaximumDate range </summary>
        public Color DisabledDayColor
        {
            get => (Color)GetValue(DisabledDayColorProperty);
            set => SetValue(DisabledDayColorProperty, value);
        }

        public static readonly BindableProperty AnimateCalendarProperty =
            BindableProperty.Create(nameof(AnimateCalendar), typeof(bool), typeof(Calendar), true);

        public bool AnimateCalendar
        {
            get => (bool)GetValue(AnimateCalendarProperty);
            set { SetValue(AnimateCalendarProperty, value); }
        }


        #region Range Selection
        public static readonly BindableProperty RangeSelectionStartDateProperty =
          BindableProperty.Create(nameof(RangeSelectionStartDate), typeof(DateTime?), typeof(MonthDaysView), DateTime.Today, BindingMode.TwoWay);

        public DateTime? RangeSelectionStartDate
        {
            get => (DateTime?)GetValue(RangeSelectionStartDateProperty);
            set => SetValue(RangeSelectionStartDateProperty, value);
        }


        public static readonly BindableProperty RangeSelectionEndDateProperty =
          BindableProperty.Create(nameof(RangeSelectionEndDate), typeof(DateTime?), typeof(MonthDaysView), DateTime.Today.AddDays(5), BindingMode.TwoWay);

        public DateTime? RangeSelectionEndDate
        {
            get => (DateTime?)GetValue(RangeSelectionEndDateProperty);
            set => SetValue(RangeSelectionEndDateProperty, value);
        }


        public static readonly BindableProperty RangeSelectionEnabledProperty =
            BindableProperty.Create(nameof(RangeSelectionEnabled), typeof(bool), typeof(MonthDaysView), false);

        public bool RangeSelectionEnabled
        {
            get => (bool)GetValue(RangeSelectionEnabledProperty);
            set => SetValue(RangeSelectionEnabledProperty, value);
        }
        #endregion
        #endregion

        private readonly Dictionary<string, bool> _propertyChangedNotificationSupressions = new Dictionary<string, bool>();
        private readonly List<DayView> _dayViews = new List<DayView>();
        private List<DayModel> _selectedRange = new List<DayModel>();
        private DayModel _selectedDay;
        private DayModel _rangeSelectionStartDay;
        private DayModel _rangeSelectionEndDay;
        private DateTime _lastAnimationTime;
        private bool _animating;

        internal MonthDaysView()
        {
            InitializeComponent();

            InitializeDays();
            UpdateDaysColors();
            UpdateDayTitles();
            UpdateDays(AnimateCalendar);
        }

        /// <summary> ??? </summary>
        ~MonthDaysView() => DiposeDayViews();

        #region PropertyChanged

        /// <summary> Method that is called when a bound property is changed. </summary>
        /// <param name="propertyName">The name of the bound property that changed.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyChangedNotificationSupressions.TryGetValue(propertyName, out bool isSuppressed)
                && isSuppressed)
                return;

            switch (propertyName)
            {
                case nameof(SelectedDate):
                    UpdateSelectedDate();
                    break;

                case nameof(DisplayedMonthYear):
                case nameof(Events):
                case nameof(MinimumDate):
                case nameof(MaximumDate):
                case nameof(OtherMonthDayIsVisible):
                case nameof(RangeSelectionStartDate):
                case nameof(RangeSelectionEndDate):                    UpdateDays(AnimateCalendar);
                    break;

                case nameof(SelectedDayTextColor):
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
                    UpdateDayTitles();
                    UpdateDays(AnimateCalendar);
                    break;

                case nameof(DaysTitleMaximumLength):
                    UpdateDayTitles();
                    break;
            }
        }

        private void UpdateDayTitles()
        {
            var dayNumber = (int)Culture.DateTimeFormat.FirstDayOfWeek;

            foreach (var dayLabel in daysTitleControl.Children.OfType<Label>())
            {
                var abberivatedDayName = Culture.DateTimeFormat.AbbreviatedDayNames[dayNumber];
                dayLabel.Text = abberivatedDayName.ToUpper().Substring(0, (int)DaysTitleMaximumLength > abberivatedDayName.Length ? abberivatedDayName.Length: (int)DaysTitleMaximumLength);
                dayNumber = (dayNumber + 1) % 7;
            }
        }

        internal void UpdateDays(bool animate)
        {
            if (Culture == null)
                return;

            Animate(() => daysControl.FadeTo(animate ? 0 : 1, 50),
                    () => daysControl.FadeTo(1, 200),
                    () => LoadDays(),
                    _lastAnimationTime = DateTime.UtcNow,
                    () => UpdateDays(false));//send false to prevent flashing if several property bindings are changed
        }

        private void UpdateDaysIsVisible()
        {
            foreach (var dayView in _dayViews)
            {
                var dayModel = dayView.BindingContext as DayModel;


                AssignIndicatorColors(ref dayModel);
            }
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
                dayModel.TodayOutlineColor = TodayOutlineColor;
                dayModel.TodayFillColor = TodayFillColor;
                dayModel.DisabledColor = DisabledDayColor;

                AssignIndicatorColors(ref dayModel);
            }
        }

        private void UpdateSelectedDate()
        {
            if (RangeSelectionEnabled)
                return;

            if (_selectedDay is null)
                _selectedDay.IsSelected = false;

            _selectedDay = _dayViews.Select(x => x.BindingContext as DayModel)
                                    .FirstOrDefault(x => x.Date == SelectedDate.Date);

            if (_selectedDay is null || !_selectedDay.IsThisMonth)
            {
                DisplayedMonthYear = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
                return;
            }

            _selectedDay.IsSelected = true;
        }

        private void OnDayModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(DayModel.IsSelected) || sender is not DayModel newSelected ||
                (_propertyChangedNotificationSupressions.TryGetValue(e.PropertyName, out bool isSuppressed) && isSuppressed))
                return;

            if (!RangeSelectionEnabled)
                SelectSingleDate(newSelected);
            else
                SelectDateRange(newSelected);
        }

        private void SelectSingleDate(DayModel newSelected)
        {
            ChangePropertySilently(nameof(SelectedDate), () => SelectedDate = newSelected.Date);

            if (!newSelected.IsThisMonth)
                DisplayedMonthYear = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            else
            {
                if (_selectedDay is null)
                    _selectedDay.IsSelected = false;

                _selectedDay = newSelected;
            }
        }

        private void SelectDateRange(DayModel newSelected)
        {
            if (_rangeSelectionStartDay is not null && _rangeSelectionEndDay is null)
                SelectRangeEndDate(newSelected);
            else
                SelectRangeStartDate(newSelected);
        }

        private void SelectRangeStartDate(DayModel newSelected)
        {
            ChangePropertySilently(nameof(RangeSelectionStartDate), () => RangeSelectionStartDate = newSelected.Date);
            ChangePropertySilently(nameof(RangeSelectionEndDate), () => RangeSelectionEndDate = null);

            _dayViews.Select(x => x.BindingContext as DayModel).ToList().ForEach(a =>
               ChangePropertySilently(nameof(DayModel.IsSelected), () => a.IsSelected = false));

            _selectedRange.Clear();
            ChangePropertySilently(nameof(DayModel.IsSelected), () => newSelected.IsSelected = true);

            _rangeSelectionStartDay.IsSelected = false;
            _rangeSelectionEndDay.IsSelected = false;
            _rangeSelectionStartDay = newSelected;
            _rangeSelectionEndDay = null;
        }

        private void SelectRangeEndDate(DayModel newSelected)
        {
            SetRangeSelectionBorderDates(newSelected);

            if (!newSelected.IsThisMonth)
                DisplayedMonthYear = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);

            var dateList = Enumerable.Range(0, RangeSelectionEndDate.Value.Subtract(RangeSelectionStartDate.Value).Days)
                                 .Select(offset => RangeSelectionStartDate.Value.AddDays(offset))
                                 .Select(a => a.Date).ToList();

            dateList.RemoveAt(0);
            _selectedRange.Clear();
            SelectDatesInList(dateList);
        }

        private void SetRangeSelectionBorderDates(DayModel newSelected)
        {
            if (DateTime.Compare(newSelected.Date, _rangeSelectionStartDay.Date) > 0)
            {
                ChangePropertySilently(nameof(RangeSelectionEndDate), () => RangeSelectionEndDate = newSelected.Date);
                _rangeSelectionEndDay = newSelected;
                return;
            }

            ChangePropertySilently(nameof(RangeSelectionEndDate), () => RangeSelectionEndDate = _rangeSelectionStartDay.Date);
            _rangeSelectionEndDay = _rangeSelectionStartDay;

            ChangePropertySilently(nameof(RangeSelectionStartDate), () => RangeSelectionStartDate = newSelected.Date);
            _rangeSelectionStartDay = newSelected;
        }

        private void SelectDatesInList(List<DateTime> listOfDates)
        {
            foreach (var date in listOfDates)
            {
                var dateToAdd = _dayViews.Select(x => x.BindingContext as DayModel).FirstOrDefault(x => x.Date == date.Date);

                if (dateToAdd is null)
                    continue;

                _selectedRange.Add(dateToAdd);
                ChangePropertySilently(nameof(DayModel.IsSelected), () => dateToAdd.IsSelected = true);
            }
        }

        #endregion

        private void InitializeDays()
        {
            foreach (var dayView in daysControl.Children.OfType<DayView>())
            {
                var dayModel = new DayModel();

                dayView.BindingContext = dayModel;
                dayModel.PropertyChanged += OnDayModelPropertyChanged;

                _dayViews.Add(dayView);
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
            var monthStart = new DateTime(DisplayedMonthYear.Year, DisplayedMonthYear.Month, 1);
            var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)monthStart.DayOfWeek;

            if (addDays > 0)
                addDays -= 7;
            _rangeSelectionStartDay = _rangeSelectionEndDay = null;
            foreach (var dayView in _dayViews)
            {
                var currentDate = monthStart.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate.Date;
                dayModel.DayViewSize = DayViewSize;
                dayModel.DayViewCornerRadius = DayViewCornerRadius;
                dayModel.DayTappedCommand = DayTappedCommand;
                dayModel.DaysLabelStyle = DaysLabelStyle;
                dayModel.EventIndicatorType = EventIndicatorType;
                dayModel.IsThisMonth = currentDate.Month == DisplayedMonthYear.Month;
                dayModel.OtherMonthIsVisible = OtherMonthDayIsVisible;
                dayModel.IsSelected = currentDate == SelectedDate.Date;
                dayModel.HasEvents = Events.ContainsKey(currentDate);
                dayModel.IsDisabled = currentDate < MinimumDate || currentDate > MaximumDate;

                if (RangeSelectionEnabled)
                {
                    if (currentDate <= RangeSelectionEndDate && currentDate >= RangeSelectionStartDate ||
                        currentDate == RangeSelectionStartDate)
                    {
                        if (currentDate == RangeSelectionStartDate)
                            _rangeSelectionStartDay = dayModel;
                        else if (currentDate == RangeSelectionEndDate)
                            _rangeSelectionEndDay = dayModel;
                        _selectedRange.Add(dayModel);
                        ChangePropertySilently(nameof(DayModel.IsSelected), () => dayModel.IsSelected = true);
                    }
                    else ChangePropertySilently(nameof(DayModel.IsSelected), () => dayModel.IsSelected = false);
                }
                else dayModel.IsSelected = currentDate == SelectedDate.Date;

                AssignIndicatorColors(ref dayModel);

                if (dayModel.IsSelected)
                    _selectedDay = dayModel;
            }
            if (RangeSelectionEnabled)
            {
                if (_rangeSelectionStartDay == null && RangeSelectionStartDate != null) 
                    _rangeSelectionStartDay = new DayModel() { Date = RangeSelectionStartDate.Value };
                if (_rangeSelectionEndDay == null && RangeSelectionEndDate != null)
                    _rangeSelectionEndDay = new DayModel() { Date = RangeSelectionEndDate.Value };
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

        private void ChangePropertySilently(string propertyName, Action propertyChangeAction)
        {
            _propertyChangedNotificationSupressions[propertyName] = true;
            propertyChangeAction();

            _propertyChangedNotificationSupressions[propertyName] = false;
        }

        private void AssignIndicatorColors(ref DayModel dayModel)
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