using System;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Models
{
    internal class DayModel : BindableBase<DayModel>
    {
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set => SetProperty(value)
                    .Notify(x => x.IsToday);
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
                    .Notify(x => x.TextColor);
        }

        public bool IsSelected
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(x => x.TextColor)
                    .Notify(x => x.BackgroundColor)
                    .Notify(x => x.EventColor)
                    .Notify(x => x.IsToday);
        }

        public Color SelectedTextColor
        {
            get => GetProperty(Color.White);
            set => SetProperty(value)
                    .Notify(x => x.TextColor);
        }

        public Color OtherMonthColor
        {
            get => GetProperty(Color.Silver);
            set => SetProperty(value)
                    .Notify(x => x.TextColor);
        }

        public Color DeselectedTextColor
        {
            get => GetProperty(Color.Default);
            set => SetProperty(value)
                    .Notify(x => x.TextColor);
        }

        public Color SelectedBackgroundColor
        {
            get => GetProperty(Color.FromHex("#2196F3"));
            set => SetProperty(value)
                    .Notify(x => x.BackgroundColor);
        }

        public Color DeselectedBackgroundColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value)
                    .Notify(x => x.BackgroundColor);
        }

        public Color EventIndicatorColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(x => x.EventColor);
        }

        public Color EventIndicatorSelectedColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(x => x.EventColor);
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
