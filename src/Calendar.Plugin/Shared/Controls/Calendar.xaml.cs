using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Controls.SelectionEngines;
using Xamarin.Plugin.Calendar.Enums;
using Xamarin.Plugin.Calendar.Models;
using Xamarin.Plugin.Calendar.Shared.Controls.ViewLayoutEngines;
using Xamarin.Plugin.Calendar.Shared.Interfaces;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Calendar plugin for Xamarin.Forms
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calendar : ContentView
    {
        #region Bindable properties

        /// <summary>
        /// Bindable property for ShowMonthPicker
        /// </summary>
        public static readonly BindableProperty ShowMonthPickerProperty =
          BindableProperty.Create(nameof(ShowMonthPicker), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Determines whether the monthPicker should be shown
        /// </summary>
        public bool ShowMonthPicker
        {
            get => (bool)GetValue(ShowMonthPickerProperty);
            set => SetValue(ShowMonthPickerProperty, value);
        }

        /// <summary>
        /// Bindable property for ShowYearPicker
        /// </summary>
        public static readonly BindableProperty ShowYearPickerProperty =
          BindableProperty.Create(nameof(ShowYearPicker), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Determines whether the yearPicker should be shown
        /// </summary>
        public bool ShowYearPicker
        {
            get => (bool)GetValue(ShowYearPickerProperty);
            set => SetValue(ShowYearPickerProperty, value);
        }

        /// <summary>
        /// Bindable property for Day
        /// </summary>
        public static readonly BindableProperty DayProperty =
          BindableProperty.Create(nameof(Day), typeof(int), typeof(Calendar), DateTime.Today.Day, BindingMode.TwoWay, propertyChanged: OnDayChanged);

        /// <summary>
        /// Number signifying the day currently selected in the picker
        /// </summary>
        public int Day
        {
            get => (int)GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }

        /// <summary>
        /// Bindable property for Month
        /// </summary>
        public static readonly BindableProperty MonthProperty =
          BindableProperty.Create(nameof(Month), typeof(int), typeof(Calendar), DateTime.Today.Month, BindingMode.TwoWay, propertyChanged: OnMonthChanged);

        /// <summary>
        /// Number signifying the month currently selected in the picker
        /// </summary>
        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        /// <summary>
        /// Bindable property for YearProperty
        /// </summary>
        public static readonly BindableProperty YearProperty =
          BindableProperty.Create(nameof(Year), typeof(int), typeof(Calendar), DateTime.Today.Year, BindingMode.TwoWay, propertyChanged: OnYearChanged);

        /// <summary>
        /// Number signifying the year currently selected in the picker
        /// </summary>
        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        /// <summary>
        /// Bindable property for InitalDate
        /// </summary>
        public static readonly BindableProperty ShownDateProperty =
          BindableProperty.Create(nameof(ShownDate), typeof(DateTime), typeof(Calendar), DateTime.Today, BindingMode.TwoWay, propertyChanged: OnDateChanged);

        /// <summary>
        /// Specifies the Date that is initially shown
        /// </summary>
        public DateTime ShownDate
        {
            get => (DateTime)GetValue(ShownDateProperty);
            set => SetValue(ShownDateProperty, value);
        }

        /// <summary>
        /// Bindable property for Culture
        /// </summary>
        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(Calendar), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        /// <summary>
        /// Specifies the culture to be used
        /// </summary>
        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        /// <summary>
        /// Bindable property for events
        /// </summary>
        public static readonly BindableProperty EventsProperty =
          BindableProperty.Create(nameof(Events), typeof(EventCollection), typeof(Calendar), new EventCollection(), propertyChanged: OnEventsChanged);

        /// <summary>
        /// Collection of all the events in the calendar
        /// </summary>
        public EventCollection Events
        {
            get => (EventCollection)GetValue(EventsProperty);
            set => SetValue(EventsProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDayEvents
        /// </summary>
        public static readonly BindableProperty SelectedDayEventsProperty =
          BindableProperty.Create(nameof(SelectedDayEvents), typeof(ICollection), typeof(Calendar), new List<object>());

        /// <summary>
        /// Collection of events on the selected date(s)
        /// </summary>
        public ICollection SelectedDayEvents
        {
            get => (ICollection)GetValue(SelectedDayEventsProperty);
            set => SetValue(SelectedDayEventsProperty, value);
        }

        /// <summary>
        /// Bindable property for EventTemplate
        /// </summary>
        public static readonly BindableProperty EventTemplateProperty =
          BindableProperty.Create(nameof(EventTemplate), typeof(DataTemplate), typeof(Calendar), null);

        /// <summary>
        /// Specifies the template to be used for showing events
        /// </summary>
        public DataTemplate EventTemplate
        {
            get => (DataTemplate)GetValue(EventTemplateProperty);
            set => SetValue(EventTemplateProperty, value);
        }

        /// <summary>
        /// Bindable property for EmptyTemplate
        /// </summary>
        public static readonly BindableProperty EmptyTemplateProperty =
            BindableProperty.Create(nameof(EmptyTemplate), typeof(DataTemplate), typeof(Calendar), null);

        /// <summary>
        /// Specifies the data template to be shown when there are no events
        /// </summary>
        public DataTemplate EmptyTemplate
        {
            get => (DataTemplate)GetValue(EmptyTemplateProperty);
            set => SetValue(EmptyTemplateProperty, value);
        }

        /// <summary>
        /// Bindable property for MonthLabelColor
        /// </summary>
        public static readonly BindableProperty MonthLabelColorProperty =
          BindableProperty.Create(nameof(MonthLabelColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        /// <summary>
        /// Specifies the color of the month label
        /// </summary>
        public Color MonthLabelColor
        {
            get => (Color)GetValue(MonthLabelColorProperty);
            set => SetValue(MonthLabelColorProperty, value);
        }

        /// <summary>
        /// Bindable property for YearLabelColor
        /// </summary>
        public static readonly BindableProperty YearLabelColorProperty =
          BindableProperty.Create(nameof(YearLabelColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        /// <summary>
        /// Specifies the color of the year label
        /// </summary>
        public Color YearLabelColor
        {
            get => (Color)GetValue(YearLabelColorProperty);
            set => SetValue(YearLabelColorProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDateColor
        /// </summary>
        public static readonly BindableProperty SelectedDateColorProperty =
          BindableProperty.Create(nameof(SelectedDateColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        /// <summary>
        /// Specifies the text color for the selected date
        /// </summary>
        public Color SelectedDateColor
        {
            get => (Color)GetValue(SelectedDateColorProperty);
            set => SetValue(SelectedDateColorProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleColor
        /// </summary>
        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Specifies the color for the titles of days
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
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(Calendar), Color.White);

        /// <summary>
        /// Specifies the text color for the titles of days
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
          BindableProperty.Create(nameof(DeselectedDayTextColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Specifies the text color for deselected days
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
          BindableProperty.Create(nameof(SelectedTodayTextColor), typeof(Color), typeof(Calendar), Color.Transparent);

        /// <summary>
        /// Specifies the text color of today's date when selected
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
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(Calendar), Color.Silver);

        /// <summary>
        /// Specifies the color of days belonging to a month other than the selected one
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
          BindableProperty.Create(nameof(OtherMonthDayIsVisible), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies whether the days belonging to a month other than the selected one will be shown
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
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(Calendar), Color.FromHex("#2196F3"));

        /// <summary>
        /// Specifies the background color of selected days
        /// </summary>
        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorType
        /// </summary>
        public static readonly BindableProperty EventIndicatorTypeProperty =
          BindableProperty.Create(nameof(EventIndicatorType), typeof(EventIndicatorType), typeof(MonthDaysView), EventIndicatorType.BottomDot);

        /// <summary>
        /// Specifies the way in which events will be shown on dates
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
          BindableProperty.Create(nameof(EventIndicatorColor), typeof(Color), typeof(Calendar), Color.FromHex("#FF4081"));

        /// <summary>
        /// Specifies the color of the event indicators
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
          BindableProperty.Create(nameof(EventIndicatorSelectedColor), typeof(Color), typeof(Calendar), Color.FromHex("#FF4081"));

        /// <summary>
        /// Specifies the color of the event indicators on selected dates
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
         BindableProperty.Create(nameof(EventIndicatorTextColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Specifies the color of the event indicator text
        /// </summary>
        public Color EventIndicatorTextColor
        {
            get => (Color)GetValue(EventIndicatorTextColorProperty);
            set => SetValue(EventIndicatorTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for EventIndicatorSelectedText
        /// </summary>
        public static readonly BindableProperty EventIndicatorSelectedTextColorProperty =
          BindableProperty.Create(nameof(EventIndicatorSelectedTextColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Specifies the color of the event indicator text on selected dates
        /// </summary>
        public Color EventIndicatorSelectedTextColor
        {
            get => (Color)GetValue(EventIndicatorSelectedTextColorProperty);
            set => SetValue(EventIndicatorSelectedTextColorProperty, value);
        }

        /// <summary>
        /// Bindable property for ArrowsColor
        /// </summary>
        public static readonly BindableProperty ArrowsColorProperty =
          BindableProperty.Create(nameof(ArrowsColor), typeof(Color), typeof(Calendar), Color.Default);

        /// <summary>
        /// Specifies the color of month and year selection arrows
        /// </summary>
        public Color ArrowsColor
        {
            get => (Color)GetValue(ArrowsColorProperty);
            set => SetValue(ArrowsColorProperty, value);
        }

        /// <summary>
        /// Bindable property for FooterArrowVisible
        /// </summary>
        public static readonly BindableProperty FooterArrowVisibleProperty =
            BindableProperty.Create(nameof(FooterArrowVisible), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies whether the footer expanding arrow is visible
        /// </summary>
        public bool FooterArrowVisible
        {
            get => (bool)GetValue(FooterArrowVisibleProperty);
            set => SetValue(FooterArrowVisibleProperty, value);
        }

        /// <summary>
        /// Bindable property for HeaderSectionVisible
        /// </summary>
        public static readonly BindableProperty HeaderSectionVisibleProperty =
            BindableProperty.Create(nameof(HeaderSectionVisible), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies whether the header section is visible
        /// </summary>
        public bool HeaderSectionVisible
        {
            get => (bool)GetValue(HeaderSectionVisibleProperty);
            set => SetValue(HeaderSectionVisibleProperty, value);
        }

        /// <summary>
        /// Bindable property for FooterSectionVisible
        /// </summary>
        public static readonly BindableProperty FooterSectionVisibleProperty =
            BindableProperty.Create(nameof(FooterSectionVisible), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies whether the footer section is visible
        /// </summary>
        public bool FooterSectionVisible
        {
            get => (bool)GetValue(FooterSectionVisibleProperty);
            set => SetValue(FooterSectionVisibleProperty, value);
        }

        /// <summary>
        /// Bindable property for TodayOutlineColor
        /// </summary>
        public static readonly BindableProperty TodayOutlineColorProperty =
          BindableProperty.Create(nameof(TodayOutlineColor), typeof(Color), typeof(Calendar), Color.FromHex("#FF4081"));

        /// <summary>
        /// Specifies the color of outline for today's date
        /// </summary>
        public Color TodayOutlineColor
        {
            get => (Color)GetValue(TodayOutlineColorProperty);
            set => SetValue(TodayOutlineColorProperty, value);
        }

        /// <summary>
        /// Bindable property for
        /// </summary>
        public static readonly BindableProperty TodayTextColorProperty =
            BindableProperty.Create(nameof(TodayTextColor), typeof(Color?), typeof(Calendar), Color.Transparent);

        /// <summary>
        /// Specifies the color of text for today's date
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
          BindableProperty.Create(nameof(TodayFillColor), typeof(Color), typeof(Calendar), Color.Transparent);

        /// <summary>
        /// Specifies the fill for today's date
        /// </summary>
        public Color TodayFillColor
        {
            get => (Color)GetValue(TodayFillColorProperty);
            set => SetValue(TodayFillColorProperty, value);
        }

        /// <summary>
        /// Bindable property for HeaderSectionTemplate
        /// </summary>
        public static readonly BindableProperty HeaderSectionTemplateProperty =
          BindableProperty.Create(nameof(HeaderSectionTemplate), typeof(DataTemplate), typeof(Calendar), new DataTemplate(() => new DefaultHeaderSection()));

        /// <summary>
        /// Data template for the header section
        /// </summary>
        public DataTemplate HeaderSectionTemplate
        {
            get => (DataTemplate)GetValue(HeaderSectionTemplateProperty);
            set => SetValue(HeaderSectionTemplateProperty, value);
        }

        /// <summary>
        /// Bindable property for FooterSectionTemplate
        /// </summary>
        public static readonly BindableProperty FooterSectionTemplateProperty =
          BindableProperty.Create(nameof(FooterSectionTemplate), typeof(DataTemplate), typeof(Calendar), new DataTemplate(() => new DefaultFooterSection()));

        /// <summary>
        /// Data template for the footer section
        /// </summary>
        public DataTemplate FooterSectionTemplate
        {
            get => (DataTemplate)GetValue(FooterSectionTemplateProperty);
            set => SetValue(FooterSectionTemplateProperty, value);
        }

        /// <summary>
        /// Bindable property for MonthText
        /// </summary>
        public static readonly BindableProperty MonthTextProperty =
          BindableProperty.Create(nameof(LayoutUnitText), typeof(string), typeof(Calendar), null);

        /// <summary>
        /// Culture specific text specifying the name of the month
        /// </summary>
        public string LayoutUnitText
        {
            get => (string)GetValue(MonthTextProperty);
            set => SetValue(MonthTextProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDateText
        /// </summary>
        public static readonly BindableProperty SelectedDateTextProperty =
          BindableProperty.Create(nameof(SelectedDateText), typeof(string), typeof(Calendar), null);

        /// <summary>
        /// Text showing which dates are currently selected
        /// </summary>
        public string SelectedDateText
        {
            get => (string)GetValue(SelectedDateTextProperty);
            set => SetValue(SelectedDateTextProperty, value);
        }

        /// <summary>
        /// Bindable property for SelectedDateTextFormat
        /// </summary>
        public static readonly BindableProperty SelectedDateTextFormatProperty =
          BindableProperty.Create(nameof(SelectedDateTextFormat), typeof(string), typeof(Calendar), "d MMM yyyy");

        /// <summary>
        /// Specifies the format of selected date text
        /// </summary>
        public string SelectedDateTextFormat
        {
            get => (string)GetValue(SelectedDateTextFormatProperty);
            set => SetValue(SelectedDateTextFormatProperty, value);
        }

        /// <summary>
        /// Binding property for CalendarSectionShown
        /// </summary>
        public static readonly BindableProperty CalendarSectionShownProperty =
          BindableProperty.Create(nameof(CalendarSectionShown), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Specifies whether the calendar section is shown
        /// </summary>
        public bool CalendarSectionShown
        {
            get => (bool)GetValue(CalendarSectionShownProperty);
            set => SetValue(CalendarSectionShownProperty, value);
        }

        /// <summary>
        /// Bindable property for DayViewSize
        /// </summary>
        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(Calendar), 40.0);

        /// <summary>
        /// Specifies the size of individual dates
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
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(Calendar), 20f);

        /// <summary>
        /// Specifies the corner radius of individual dates
        /// </summary>
        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleMaximumLength
        /// </summary>
        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
          BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(Calendar), DaysTitleMaxLength.ThreeChars, propertyChanged: T);

        private static void T(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar vm)
                vm.monthDaysView.DaysTitleMaximumLength = (DaysTitleMaxLength)newValue;
        }

        /// <summary>
        /// Specifies the maximum length of weekday titles
        /// </summary>
        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysTitleHeight
        /// </summary>
        public static readonly BindableProperty DaysTitleHeightProperty =
          BindableProperty.Create(nameof(DaysTitleHeight), typeof(double), typeof(Calendar), 30.0);

        /// <summary>
        /// Specifies the height of weekday titles
        /// </summary>
        public double DaysTitleHeight
        {
            get => (double)GetValue(DaysTitleHeightProperty);
            set => SetValue(DaysTitleHeightProperty, value);
        }

        /// <summary>
        /// Bindable property for DaysLabelStyle
        /// </summary>
        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(Calendar), Device.Styles.BodyStyle);

        /// <summary>
        /// Specifies the style of day labels
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
          BindableProperty.Create(nameof(DaysTitleLabelStyle), typeof(Style), typeof(Calendar), null);

        /// <summary>
        /// Specifies the style of day title labels
        /// </summary>
        public Style DaysTitleLabelStyle
        {
            get => (Style)GetValue(DaysTitleLabelStyleProperty);
            set => SetValue(DaysTitleLabelStyleProperty, value);
        }

        /// <summary>
        /// Bindable property for DisableSwipeDetection
        /// </summary>
        public static readonly BindableProperty DisableSwipeDetectionProperty =
          BindableProperty.Create(nameof(DisableSwipeDetection), typeof(bool), typeof(Calendar), false);

        /// <summary>
        /// <para> Disables the swipe detection (needs testing on iOS) </para>
        /// Could be useful if your superview has its own swipe-detection logic.
        /// Also see if <seealso cref="SwipeUpCommand"/>, <seealso cref="SwipeUpToHideEnabled"/>, <seealso cref="SwipeLeftCommand"/>, <seealso cref="SwipeRightCommand"/> or <seealso cref="SwipeToChangeMonthEnabled"/> is useful to you.
        /// </summary>
        public bool DisableSwipeDetection
        {
            get => (bool)GetValue(DisableSwipeDetectionProperty);
            set => SetValue(DisableSwipeDetectionProperty, value);
        }

        /// <summary>
        /// Bindable property for SwipeUpCommand
        /// </summary>
        public static readonly BindableProperty SwipeUpCommandProperty =
          BindableProperty.Create(nameof(SwipeUpCommand), typeof(ICommand), typeof(Calendar), null);

        /// <summary>
        /// Activated when user swipes-up over days view
        /// </summary>
        public ICommand SwipeUpCommand
        {
            get => (ICommand)GetValue(SwipeUpCommandProperty);
            set => SetValue(SwipeUpCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for SwipeUpToHideEnabled
        /// </summary>
        public static readonly BindableProperty SwipeUpToHideEnabledProperty =
          BindableProperty.Create(nameof(SwipeUpToHideEnabled), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Enable/disable default swipe-up action for showing/hiding calendar
        /// </summary>
        public bool SwipeUpToHideEnabled
        {
            get => (bool)GetValue(SwipeUpToHideEnabledProperty);
            set => SetValue(SwipeUpToHideEnabledProperty, value);
        }

        /// <summary>
        /// Bindable property for SwipeLeftCommand
        /// </summary>
        public static readonly BindableProperty SwipeLeftCommandProperty =
          BindableProperty.Create(nameof(SwipeLeftCommand), typeof(ICommand), typeof(Calendar), null);

        /// <summary>
        /// Activated when user swipes-left over days view
        /// </summary>
        public ICommand SwipeLeftCommand
        {
            get => (ICommand)GetValue(SwipeLeftCommandProperty);
            set => SetValue(SwipeLeftCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for SwipeRightCommand
        /// </summary>
        public static readonly BindableProperty SwipeRightCommandProperty =
          BindableProperty.Create(nameof(SwipeRightCommand), typeof(ICommand), typeof(Calendar), null);

        /// <summary>
        /// Activated when user swipes-right over days view
        /// </summary>
        public ICommand SwipeRightCommand
        {
            get => (ICommand)GetValue(SwipeRightCommandProperty);
            set => SetValue(SwipeRightCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for SwipeToChangeMonthEnabled
        /// </summary>
        public static readonly BindableProperty SwipeToChangeMonthEnabledProperty =
          BindableProperty.Create(nameof(SwipeToChangeMonthEnabled), typeof(bool), typeof(Calendar), true);

        /// <summary>
        /// Enable/disable default swipe actions for changing months
        /// </summary>
        public bool SwipeToChangeMonthEnabled
        {
            get => (bool)GetValue(SwipeToChangeMonthEnabledProperty);
            set => SetValue(SwipeToChangeMonthEnabledProperty, value);
        }

        /// <summary>
        /// Bindable property for DayTapped
        /// </summary>
        public static readonly BindableProperty DayTappedCommandProperty =
            BindableProperty.Create(nameof(DayTappedCommand), typeof(ICommand), typeof(Calendar), null);

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
          BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(Calendar), DateTime.MinValue);

        /// <summary> Minimum date which can be selected </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary> Bindable property for MaximumDate </summary>
        public static readonly BindableProperty MaximumDateProperty =
          BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(Calendar), DateTime.MaxValue);

        /// <summary> Maximum date which can be selected </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary> Bindable property for DisabledDayColor </summary>
        public static readonly BindableProperty DisabledDayColorProperty =
          BindableProperty.Create(nameof(DisabledDayColor), typeof(Color), typeof(Calendar), Color.FromHex("#ECECEC"));

        /// <summary> Color for days which are out of MinimumDate - MaximumDate range </summary>
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
        /// Specifies whether the calendar is animated
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
            BindableProperty.Create(nameof(CalendarLayout), typeof(WeekLayout), typeof(Calendar), WeekLayout.Month, propertyChanged: OnCalendarLayoutChanged);

        /// <summary>
        /// Sets the layout of the calendar
        /// </summary>
        public WeekLayout CalendarLayout
        {
            get => (WeekLayout)GetValue(CalendarLayoutProperty);
            set => SetValue(CalendarLayoutProperty, value);
        }

        /// <summary>
        /// Bindable property for WeekViewUnit
        /// </summary>
        public static readonly BindableProperty WeekViewUnitProperty =
            BindableProperty.Create(nameof(WeekViewUnit), typeof(WeekViewUnit), typeof(Calendar), WeekViewUnit.MonthName, propertyChanged: OnWeekViewUnitChanged);

        /// <summary>
        /// Sets the display name of the calendar unit
        /// </summary>
        public WeekViewUnit WeekViewUnit
        {
            get => (WeekViewUnit)GetValue(WeekViewUnitProperty);
            set => SetValue(WeekViewUnitProperty, value);
        }

        #endregion

        #region SelectedDates

        /// <summary>
        /// Bindable property for SelectedDate
        /// </summary>
        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime?), typeof(Calendar), null, BindingMode.TwoWay, propertyChanged: OnSelectedDateChanged);

        private static void OnSelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Calendar)bindable;
            var dateToSet = (DateTime?)newValue;

            control.SetValue(SelectedDateProperty, dateToSet);
            if (!control._isSelectingDates || control.monthDaysView.CurrentSelectionEngine is SingleSelectionEngine)
            {
                if (dateToSet.HasValue)
                    control.SetValue(SelectedDatesProperty, new List<DateTime> { dateToSet.Value });
                else
                    control.SetValue(SelectedDatesProperty, new List<DateTime>());
            }
            else
            {
                control._isSelectingDates = false;
            }
        }

        /// <summary>
        /// Selected date in single date selection mode
        /// </summary>
        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set
            {
                SetValue(SelectedDatesProperty, value.HasValue ? new List<DateTime> { value.Value } : null);
                SetValue(SelectedDateProperty, value);
            }
        }

        /// <summary>
        /// Bindable property for SelectedDates
        /// </summary>
        public static readonly BindableProperty SelectedDatesProperty =
          BindableProperty.Create(nameof(SelectedDates), typeof(List<DateTime>), typeof(Calendar), null, BindingMode.TwoWay);

        private bool _isSelectingDates = false;

        /// <summary>
        /// Selected date in single date selection mode
        /// </summary>
        public List<DateTime> SelectedDates
        {
            get => (List<DateTime>)GetValue(SelectedDatesProperty);
            set
            {
                SetValue(SelectedDatesProperty, value);
                _isSelectingDates = true;
                SetValue(SelectedDateProperty, value?.Count > 0 ? value.First() : null);
            }
        }

        #endregion

        private const uint CalendarSectionAnimationRate = 16;
        private const int CalendarSectionAnimationDuration = 200;
        private const string CalendarSectionAnimationId = nameof(CalendarSectionAnimationId);
        private readonly Animation _calendarSectionAnimateHide;
        private readonly Animation _calendarSectionAnimateShow;
        private bool _calendarSectionAnimating;
        private double _calendarSectionHeight;
        private IViewLayoutEngine _viewLayoutEngine = new MonthViewEngine(CultureInfo.InvariantCulture);

        /// <summary>
        /// Calendar plugin for Xamarin.Forms
        /// </summary>
        public Calendar()
        {
            PrevLayoutUnitCommand = new Command(PrevUnit);
            NextLayoutUnitCommand = new Command(NextUnit);
            PrevYearCommand = new Command(PrevYear);
            NextYearCommand = new Command(NextYear);
            ShowHideCalendarCommand = new Command(ToggleCalendarSectionVisibility);

            InitializeComponent();
            InitializeSelectionType();
            UpdateSelectedDateLabel();
            UpdateLayoutUnitLabel();
            UpdateEvents();

            _calendarSectionAnimateHide = new Animation(AnimateMonths, 1, 0);
            _calendarSectionAnimateShow = new Animation(AnimateMonths, 0, 1);

            calendarContainer.SizeChanged += OnCalendarContainerSizeChanged;
        }

        private void InitializeSelectionType()
        {
            monthDaysView.CurrentSelectionEngine = new SingleSelectionEngine();
        }

        #region Properties

        /// <summary>
        /// When executed calendar moves to previous week/month.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="FooterSectionTemplate"/>
        /// </summary>
        public ICommand PrevLayoutUnitCommand { get; }

        /// <summary>
        /// When executed calendar moves to next week/month.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="FooterSectionTemplate"/>
        /// </summary>
        public ICommand NextLayoutUnitCommand { get; }

        /// <summary>
        /// When executed calendar moves to previous year.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="FooterSectionTemplate"/>
        /// </summary>
        public ICommand PrevYearCommand { get; }

        /// <summary>
        /// When executed calendar moves to next year.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="FooterSectionTemplate"/>
        /// </summary>
        public ICommand NextYearCommand { get; }

        /// <summary>
        /// When executed shows/hides the calendar's current month days view.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="FooterSectionTemplate"/>
        /// </summary>
        public ICommand ShowHideCalendarCommand { get; }

        #endregion

        #region PropertyChanged

        private static void OnEventsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar view)
            {
                if (oldValue is EventCollection oldEvents)
                    oldEvents.CollectionChanged -= view.OnEventsCollectionChanged;

                if (newValue is EventCollection newEvents)
                    newEvents.CollectionChanged += view.OnEventsCollectionChanged;

                view.UpdateEvents();
                view.monthDaysView.UpdateAndAnimateDays(view.AnimateCalendar);
            }
        }

        private static void OnYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar && calendar.ShownDate.Year != (int)newValue)
                calendar.ShownDate = new DateTime((int)newValue, calendar.Month, calendar.Day);
        }

        private static void OnMonthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is not int newMonth || newMonth <= 0 || newMonth > 12)
                throw new ArgumentException("Month must be between 1 and 12.");

            if (bindable is Calendar calendar && calendar.ShownDate.Month != newMonth)
                calendar.ShownDate = new DateTime(calendar.Year, newMonth, calendar.Day);
        }

        private static void OnDayChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar && newValue is int newDay && calendar.ShownDate.Day != newDay)
                calendar.ShownDate = new DateTime(calendar.Year, calendar.Month, newDay);
        }

        private static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar && newValue is DateTime newDateTime)
            {
                if (calendar.Day != newDateTime.Day)
                    calendar.Day = newDateTime.Day;

                if (calendar.Month != newDateTime.Month)
                    calendar.Month = newDateTime.Month;

                if (calendar.Year != newDateTime.Year)
                    calendar.Year = newDateTime.Year;

                if (calendar.monthDaysView.ShownDate != calendar.ShownDate)
                    calendar.monthDaysView.ShownDate = calendar.ShownDate;
            }
        }

        private static void OnCalendarLayoutChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar && newValue is WeekLayout layout)
            {
                calendar.monthDaysView.CalendarLayout = layout;

                calendar._viewLayoutEngine = layout switch
                {
                    WeekLayout.Week => new WeekViewEngine(calendar.Culture, 1),
                    WeekLayout.TwoWeek => new WeekViewEngine(calendar.Culture, 2),
                    _ => new MonthViewEngine(calendar.Culture),
                };
            }
        }

        private static void OnWeekViewUnitChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar && newValue is WeekViewUnit viewUnit)
            {
                calendar.WeekViewUnit = viewUnit;
            }
        }

        /// <summary>
        /// Method that is called when a bound property is changed.
        /// </summary>
        /// <param name="propertyName">The name of the bound property that changed.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(ShownDate):
                    UpdateLayoutUnitLabel();
                    break;

                case nameof(SelectedDates):
                    UpdateSelectedDateLabel();
                    UpdateEvents();
                    break;

                case nameof(Culture):
                    if (ShownDate.Month > 0)
                        UpdateLayoutUnitLabel();

                    UpdateSelectedDateLabel();
                    break;

                case nameof(CalendarSectionShown):
                    ShowHideCalendarSection();
                    break;
            }
        }

        private void UpdateEvents()
        {
            SelectedDayEvents = monthDaysView.CurrentSelectionEngine.TryGetSelectedEvents(Events, out var selectedEvents) ? selectedEvents : null;

            eventsScrollView.ScrollToAsync(0, 0, false);
        }

        private void UpdateLayoutUnitLabel()
        {
            if (WeekViewUnit == WeekViewUnit.WeekNumber)
            {
                LayoutUnitText = GetWeekNumber(ShownDate).ToString();
                return;
            }

            LayoutUnitText = Culture.DateTimeFormat.MonthNames[ShownDate.Month - 1].Capitalize();
        }

        private void UpdateSelectedDateLabel()
        {
            SelectedDateText = monthDaysView.CurrentSelectionEngine.GetSelectedDateText(SelectedDateTextFormat, Culture);
        }

        private void ShowHideCalendarSection()
        {
            if (_calendarSectionAnimating)
                return;

            _calendarSectionAnimating = true;

            var animation = CalendarSectionShown ? _calendarSectionAnimateShow : _calendarSectionAnimateHide;
            var prevState = CalendarSectionShown;

            animation.Commit(this, CalendarSectionAnimationId, CalendarSectionAnimationRate, CalendarSectionAnimationDuration, finished: (value, cancelled) =>
            {
                _calendarSectionAnimating = false;

                if (prevState != CalendarSectionShown)
                    ToggleCalendarSectionVisibility();
            });
        }

        private void UpdateCalendarSectionHeight()
        {
            _calendarSectionHeight = calendarContainer.Height;
        }

        private void OnEventsCollectionChanged(object sender, EventCollection.EventCollectionChangedArgs e)
        {
            UpdateEvents();
            monthDaysView.UpdateAndAnimateDays(AnimateCalendar);
        }

        #endregion

        #region Event Handlers

        private void OnCalendarContainerSizeChanged(object sender, EventArgs e)
        {
            if (calendarContainer.Height > 0 && !_calendarSectionAnimating)
                UpdateCalendarSectionHeight();
        }

        private void OnSwipedRight(object sender, EventArgs e)
        {
            SwipeRightCommand?.Execute(null);

            if (SwipeToChangeMonthEnabled)
                PrevUnit();
        }

        private void OnSwipedLeft(object sender, EventArgs e)
        {
            SwipeLeftCommand?.Execute(null);

            if (SwipeToChangeMonthEnabled)
                NextUnit();
        }

        private void OnSwipedUp(object sender, EventArgs e)
        {
            SwipeUpCommand?.Execute(null);

            if (SwipeUpToHideEnabled)
                ToggleCalendarSectionVisibility();
        }

        #endregion

        #region Other methods

        private int GetWeekNumber(DateTime date)
        {
            return Culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, Culture.DateTimeFormat.FirstDayOfWeek);
        }

        private void PrevUnit()
        {
            var targetDate = _viewLayoutEngine.GetPreviousUnit(ShownDate);

            ShownDate = targetDate < MinimumDate
                ? MinimumDate
                : targetDate;
        }

        private void NextUnit()
        {
            var targetDate = _viewLayoutEngine.GetNextUnit(ShownDate);

            ShownDate = targetDate > MaximumDate
                ? MaximumDate
                : targetDate;
        }

        private void PrevYear(object obj)
        {
            var targetDate = ShownDate.AddYears(-1);

            ShownDate = targetDate < MinimumDate
                ? MinimumDate
                : targetDate;
        }

        private void NextYear(object obj)
        {
            var targetDate = ShownDate.AddYears(1);

            ShownDate = targetDate > MaximumDate
                ? MaximumDate
                : targetDate;
        }

        private void ToggleCalendarSectionVisibility()
            => CalendarSectionShown = !CalendarSectionShown;

        private void AnimateMonths(double currentValue)
        {
            calendarContainer.HeightRequest = _calendarSectionHeight * currentValue;
            calendarContainer.TranslationY = _calendarSectionHeight * (currentValue - 1);
            calendarContainer.Opacity = currentValue * currentValue * currentValue;
        }

        #endregion
    }
}
