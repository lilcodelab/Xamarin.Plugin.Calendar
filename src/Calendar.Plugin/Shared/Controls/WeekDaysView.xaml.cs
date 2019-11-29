using Xamarin.Plugin.Calendar.Models;
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
    public partial class WeekDaysView : ContentView
    {
        #region Bindable properties

        public static readonly BindableProperty WeekStartDateProperty =
          BindableProperty.Create(nameof(WeekStartDate), typeof(DateTime), typeof(WeekDaysView), DateTime.Now, BindingMode.TwoWay);

        public DateTime WeekStartDate
        {
            get => (DateTime)GetValue(WeekStartDateProperty);
            set => SetValue(WeekStartDateProperty, value);
        }

        public static readonly BindableProperty WeekProperty =
          BindableProperty.Create(nameof(Week), typeof(int), typeof(WeekDaysView), 1, BindingMode.TwoWay);

        public int Week
        {
            get => (int)GetValue(WeekProperty);
            set => SetValue(WeekProperty, value);
        }


        public static readonly BindableProperty MonthProperty =
          BindableProperty.Create(nameof(Month), typeof(int), typeof(WeekDaysView), DateTime.Now.Month, BindingMode.TwoWay);

        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty YearProperty =
          BindableProperty.Create(nameof(Year), typeof(int), typeof(WeekDaysView), DateTime.Now.Year, BindingMode.TwoWay);

        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(WeekDaysView), DateTime.Today, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(WeekDaysView), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty MarkDatesProperty =
          BindableProperty.Create(nameof(MarkDates), typeof(List<DateTime>), typeof(WeekDaysView), new List<DateTime>());

        public List<DateTime> MarkDates
        {
            get => (List<DateTime>)GetValue(MarkDatesProperty);
            set => SetValue(MarkDatesProperty, value);
        }

        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(WeekDaysView), new EventCollection());

        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(WeekDaysView), Color.Default);

        public Color DaysTitleColor
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(WeekDaysView), Color.White);

        public Color SelectedDayTextColor
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty DeselectedDayTextColorProperty =
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(WeekDaysView), Color.Default);

        public Color DeselectedDayTextColor
        {
            get => (Color)GetValue(DeselectedDayTextColorProperty);
            set => SetValue(DeselectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(WeekDaysView), Color.Silver);

        public Color OtherMonthDayColor
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(WeekDaysView), Color.FromHex("#2196F3"));

        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        public static readonly BindableProperty EventIndicatorColorProperty =
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(WeekDaysView), Color.FromHex("#FF4081"));

        public Color EventIndicatorColor
        {
            get => (Color)GetValue(EventIndicatorColorProperty);
            set => SetValue(EventIndicatorColorProperty, value);
        }

        public static readonly BindableProperty EventIndicatorSelectedColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedColor), typeof(Color), typeof(WeekDaysView), Color.FromHex("#FF4081"));

        public Color EventIndicatorSelectedColor
        {
            get => (Color)GetValue(EventIndicatorSelectedColorProperty);
            set => SetValue(EventIndicatorSelectedColorProperty, value);
        }

        public static readonly BindableProperty TodayOutlineColorProperty =
          BindableProperty.Create(nameof(TodayOutlineColor), typeof(Color), typeof(WeekDaysView), Color.FromHex("#FF4081"));

        public Color TodayOutlineColor
        {
            get => (Color)GetValue(TodayOutlineColorProperty);
            set => SetValue(TodayOutlineColorProperty, value);
        }

        public static readonly BindableProperty TodayFillColorProperty =
          BindableProperty.Create(nameof(TodayFillColor), typeof(Color), typeof(WeekDaysView), Color.Transparent);

        public Color TodayFillColor
        {
            get => (Color)GetValue(TodayFillColorProperty);
            set => SetValue(TodayFillColorProperty, value);
        }

        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(WeekDaysView), 40.0);

        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(WeekDaysView), 20f);

        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        public static readonly BindableProperty DaysTitleHeightProperty =
          BindableProperty.Create(nameof(DaysTitleHeight), typeof(double), typeof(WeekDaysView), 30.0);

        public double DaysTitleHeight
        {
            get => (double)GetValue(DaysTitleHeightProperty);
            set => SetValue(DaysTitleHeightProperty, value);
        }

        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(WeekDaysView), null);

        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        public static readonly BindableProperty DaysTitleLabelStyleProperty =
          BindableProperty.Create(nameof(DaysTitleLabelStyle), typeof(Style), typeof(WeekDaysView), null);

        public Style DaysTitleLabelStyle
        {
            get => (Style)GetValue(DaysTitleLabelStyleProperty);
            set => SetValue(DaysTitleLabelStyleProperty, value);
        }

        #endregion

        private readonly Dictionary<string, bool> _propertyChangedNotificationSupressions = new Dictionary<string, bool>();
        private readonly List<DayView> _dayViews = new List<DayView>();
        private DayModel _selectedDay;
        private bool _animating;
        private DateTime _lastAnimationTime;

        internal WeekDaysView()
        {
            InitializeComponent();

            InitializeDayViews();
            AssignDayViewModels();
            UpdateDaysColors();
            UpdateDayTitles();
            UpdateDays();
        }

        ~WeekDaysView()
        {
            DiposeDayViews();
        }

        #region PropertyChanged

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyChangedNotificationSupressions.TryGetValue(propertyName, out bool isSuppressed)
                && isSuppressed)
                return;

            switch (propertyName)
            {
                case nameof(SelectedDate):
                    //UpdateSelectedDate();
                    UpdateDays();
                    break;

                //case nameof(Month):
                //case nameof(Year):
                case nameof(Events):
                case nameof(Week):
                case nameof(WeekStartDate):
                    UpdateDays();
                    break;

                case nameof(SelectedDayTextColor):
                case nameof(OtherMonthDayColor):
                case nameof(DeselectedDayTextColor):
                case nameof(SelectedDayBackgroundColor):
                case nameof(EventIndicatorColor):
                case nameof(EventIndicatorSelectedColor):
                case nameof(TodayOutlineColor):
                case nameof(TodayFillColor):
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
            if (Culture.Name.Equals("zh-CN"))
            {
                dayNumber = (int)DayOfWeek.Monday;
            }

            foreach (var dayLabel in daysTitleControl.Children.OfType<Label>())
            {
                dayLabel.Text = Culture.DateTimeFormat.AbbreviatedDayNames[dayNumber].ToUpper();
                if (Culture.Name.Equals("zh-CN"))
                {
                    dayLabel.Text = dayLabel.Text.Substring(1);
                }
                dayNumber = (dayNumber + 1) % 7;
            }
        }

        internal void UpdateDays()
        {
            if (Year == 0 || Month == 0 || Culture == null)
                return;

            Animate(() => daysControl.FadeTo(0, 50),
                    () => daysControl.FadeTo(1, 200),
                    () => LoadDays(),
                    _lastAnimationTime = DateTime.UtcNow,
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
                dayModel.EventIndicatorSelectedColor = EventIndicatorSelectedColor;
                dayModel.TodayOutlineColor = TodayOutlineColor;
                dayModel.TodayFillColor = TodayFillColor;
            }
        }

        private void UpdateSelectedDate()
        {
            if (_selectedDay != null)
                _selectedDay.IsSelected = false;

            _selectedDay = _dayViews.Select(x => x.BindingContext as DayModel)
                                    .FirstOrDefault(x => x.Date == SelectedDate.Date);

            if (_selectedDay == null || !_selectedDay.IsThisMonth)
            {
                Year = SelectedDate.Date.Year;
                Month = SelectedDate.Date.Month;
                return;
            }

            _selectedDay.IsSelected = true;
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
                //ChangePropertySilently(nameof(SelectedDate), () => SelectedDate = newSelected.Date);

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



        /// <summary>
        /// 获取指定日期所在周的第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime GetWeekFirstDay(DateTime datetime)
        {
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int dayDiff = 0;
            if (Culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday && !Culture.Name.Equals("zh-CN"))
            {
                //星期天为第一天
                dayDiff = (-1) * weeknow;
            }
            else
            {
                //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
                weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
                dayDiff = (-1) * weeknow;
            }
            //本周第一天
            string FirstDay = datetime.AddDays(dayDiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        private void LoadDays()
        {
            var startDate = WeekStartDate.Date.AddDays((Week - 1) * 7);
            Month = startDate.Month;
            Year = startDate.Year;
            DateTime weekStart = GetWeekFirstDay(startDate);

            var addDays = ((int)Culture.DateTimeFormat.FirstDayOfWeek) - (int)weekStart.DayOfWeek;
            if (Culture.Name.Equals("zh-CN"))
            {
                addDays = ((int)DayOfWeek.Monday) - (int)weekStart.DayOfWeek;
            }

            if (addDays > 0)
                addDays -= 7;
            foreach (var dayView in _dayViews)
            {
                var currentDate = weekStart.AddDays(addDays++);
                var dayModel = dayView.BindingContext as DayModel;

                dayModel.Date = currentDate.Date;
                dayModel.IsThisMonth = currentDate.Month == Month;
                dayModel.IsSelected = currentDate == SelectedDate.Date;
                dayModel.HasEvents = Events.ContainsKey(currentDate) || (MarkDates.Find(x => x.Date == currentDate.Date) != DateTime.MinValue);

                if (dayModel.IsSelected)
                    _selectedDay = dayModel;
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
    }
}