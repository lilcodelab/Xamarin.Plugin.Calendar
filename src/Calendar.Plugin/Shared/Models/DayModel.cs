using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Models
{
    internal class DayModel : BindableBase<DayModel>
    {
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor),
                            nameof(OutlineColor));
        }

        public double DayViewSize
        {
            get => GetProperty<double>();
            set => SetProperty(value);
        }

        public float DayViewCornerRadius
        {
            get => GetProperty<float>();
            set => SetProperty(value);
        }

        public Style DaysLabelStyle
        {
            get => GetProperty(Device.Styles.BodyStyle);
            set => SetProperty(value);
        }

        public ICommand DayTappedCommand
        {
            get => GetProperty<ICommand>();
            set => SetProperty(value);
        }

        public bool HasEvents
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(BottomDotVisible),
                            nameof(TopDotVisible),
                            nameof(BackgroundEventIndicator),
                            nameof(BackgroundFullEventColor));
        }

        public bool IsThisMonth
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public bool IsSelected
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor),
                            nameof(BackgroundColor),
                            nameof(OutlineColor),
                            nameof(EventColor),
                            nameof(BackgroundFullEventColor));
        }

        public bool IsDisabled
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color SelectedTextColor
        {
            get => GetProperty(Color.White);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color OtherMonthColor
        {
            get => GetProperty(Color.Silver);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color DeselectedTextColor
        {
            get => GetProperty(Color.Default);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color SelectedBackgroundColor
        {
            get => GetProperty(Color.FromHex("#2196F3"));
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public Color DeselectedBackgroundColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public EventIndicatorType EventIndicatorType
        {
            get => GetProperty(EventIndicatorType.BottomDot);
            set => SetProperty(value)
                    .Notify(nameof(BottomDotVisible),
                            nameof(TopDotVisible),
                            nameof(BackgroundEventIndicator),
                            nameof(BackgroundColor));
        }

        public Color EventIndicatorColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(nameof(EventColor),
                            nameof(BackgroundColor),
                            nameof(BackgroundFullEventColor));
        }

        public Color EventIndicatorSelectedColor
        {
            get => GetProperty(SelectedBackgroundColor);
            set => SetProperty(value)
                    .Notify(nameof(EventColor),
                            nameof(BackgroundColor),
                            nameof(BackgroundFullEventColor));
        }

        public Color EventIndicatorTextColor
        {
            get => GetProperty(DeselectedTextColor);
            set => SetProperty(value);
        }

        public Color EventIndicatorSelectedTextColor
        {
            get => GetProperty(SelectedTextColor);
            set => SetProperty(value);
        }

        public Color TodayOutlineColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(nameof(OutlineColor));
        }

        public Color TodayFillColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public Color DisabledColor
        {
            get => GetProperty(Color.FromHex("#ECECEC"));
            set => SetProperty(value);
        }

        public bool BottomDotVisible => HasEvents && EventIndicatorType == EventIndicatorType.BottomDot;

        public bool TopDotVisible => HasEvents && EventIndicatorType == EventIndicatorType.TopDot;

        public bool BackgroundEventIndicator => HasEvents && EventIndicatorType == EventIndicatorType.Background;

        public Color BackgroundFullEventColor => HasEvents && EventIndicatorType == EventIndicatorType.BackgroundFull
                                               ? EventColor
                                               : Color.Default;

        public Color EventColor => IsSelected
                                 ? EventIndicatorSelectedColor
                                 : EventIndicatorColor;

        public Color OutlineColor => IsToday()
                                   ? TodayOutlineColor
                                   : Color.Transparent;


        public Color BackgroundColor =>
            (BackgroundEventIndicator, IsSelected, IsToday()) switch
            {
                (true, false, _) => EventIndicatorColor,
                (true, true, _) => EventIndicatorSelectedColor,
                (false, true, _) => SelectedBackgroundColor,
                (false, false, true) => TodayFillColor,
                (_, _, _) => DeselectedBackgroundColor
            };

        public Color TextColor =>
            (IsDisabled, IsSelected, HasEvents, IsThisMonth) switch
            {
                (true, _, _, _) => DisabledColor,
                (false, true, false, _) => SelectedTextColor,
                (false, true, true, _) => EventIndicatorSelectedTextColor,
                (false, false, true, _) => EventIndicatorTextColor,
                (false, false, false, true) => DeselectedTextColor,
                (_, _, _, _) => OtherMonthColor
            };



        private bool IsToday()
            => Date.Date == DateTime.Today && !IsSelected;
    }
}
