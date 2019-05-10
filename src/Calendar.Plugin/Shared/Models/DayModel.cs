using System;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Models
{
    internal class DayModel : BindableBase
    {
        public DayModel()
        {
            SetupPropertyDependencies(nameof(IsThisMonth), nameof(TextColor));
            SetupPropertyDependencies(nameof(IsSelected), nameof(TextColor), nameof(BackgroundColor), nameof(EventColor), nameof(IsToday));
            SetupPropertyDependencies(nameof(SelectedTextColor), nameof(TextColor));
            SetupPropertyDependencies(nameof(OtherMonthColor), nameof(TextColor));
            SetupPropertyDependencies(nameof(DeselectedTextColor), nameof(TextColor));
            SetupPropertyDependencies(nameof(SelectedBackgroundColor), nameof(BackgroundColor));
            SetupPropertyDependencies(nameof(DeselectedBackgroundColor), nameof(BackgroundColor));
            SetupPropertyDependencies(nameof(EventIndicatorColor), nameof(EventColor));
            SetupPropertyDependencies(nameof(EventIndicatorSelectedColor), nameof(EventColor));
            SetupPropertyDependencies(nameof(Date), nameof(IsToday));
        }

        public DateTime Date
        {
            get => GetProperty<DateTime>();
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
            set => SetProperty(value);
        }

        public bool IsSelected
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public Color SelectedTextColor
        {
            get => GetProperty(Color.White);
            set => SetProperty(value);
        }

        public Color OtherMonthColor
        {
            get => GetProperty(Color.Silver);
            set => SetProperty(value);
        }

        public Color DeselectedTextColor
        {
            get => GetProperty(Color.Default);
            set => SetProperty(value);
        }

        public Color SelectedBackgroundColor
        {
            get => GetProperty(Color.FromHex("#2196F3"));
            set => SetProperty(value);
        }

        public Color DeselectedBackgroundColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value);
        }

        public Color EventIndicatorColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value);
        }

        public Color EventIndicatorSelectedColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value);
        }

        public Color TodayOutlineColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value);
        }

        public Color TodayFillColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value);
        }

        public Color EventColor => IsSelected
                                 ? EventIndicatorSelectedColor
                                 : EventIndicatorColor;

        public Color BackgroundColor => IsSelected
                                      ? SelectedBackgroundColor
                                      : DeselectedBackgroundColor;

        public Color TextColor => IsSelected
                                ? SelectedTextColor
                                : IsThisMonth ? DeselectedTextColor : OtherMonthColor;

        public bool IsToday => Date.Date == DateTime.Today && !IsSelected;
    }
}
