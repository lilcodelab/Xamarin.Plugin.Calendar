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
                    .Notify(nameof(BackgroundColor),
                            nameof(OutlineColor));
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

        public Color EventColor => IsSelected
                                 ? EventIndicatorSelectedColor
                                 : EventIndicatorColor;

        public Color OutlineColor => IsToday()
                                   ? TodayOutlineColor
                                   : Color.Transparent;

        public Color BackgroundColor => IsSelected
                                      ? SelectedBackgroundColor
                                      : IsToday()
                                        ? TodayFillColor
                                        : DeselectedBackgroundColor;

        public Color TextColor => IsSelected
                                ? SelectedTextColor
                                : IsThisMonth ? DeselectedTextColor : OtherMonthColor;

        private bool IsToday()
            => Date.Date == DateTime.Today && !IsSelected;
    }
}
