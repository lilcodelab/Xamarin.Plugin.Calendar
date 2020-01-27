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
            get => GetProperty<Style>(Device.Styles.TitleStyle);
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
            set => SetProperty(value);
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
                            nameof(EventColor));
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

        public Color EventIndicatorColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(nameof(EventColor));
        }

        public Color EventIndicatorSelectedColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(nameof(EventColor));
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

        public Color EventColor => IsSelected
                                 ? EventIndicatorSelectedColor
                                 : EventIndicatorColor;

        public Color OutlineColor => IsToday()
                                   ? TodayOutlineColor
                                   : Color.Transparent;

        public Color BackgroundColor => IsSelected
                                      ? SelectedBackgroundColor
                                      : IsToday() ? TodayFillColor
                                                  : DeselectedBackgroundColor;

        public Color TextColor => IsDisabled
                                ? DisabledColor
                                : IsSelected ? SelectedTextColor
                                             : IsThisMonth ? DeselectedTextColor   
                                                           : OtherMonthColor;

        private bool IsToday()
            => Date.Date == DateTime.Today && !IsSelected;
    }
}
