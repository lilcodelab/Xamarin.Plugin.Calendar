using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Models
{
    internal class DayModel : INotifyPropertyChanged
    {
        private static readonly PropertyChangedEventArgs _datePropertyArgs = new PropertyChangedEventArgs(nameof(Date));
        private static readonly PropertyChangedEventArgs _hasEventsPropertyArgs = new PropertyChangedEventArgs(nameof(HasEvents));
        private static readonly PropertyChangedEventArgs _isThisMonthPropertyArgs = new PropertyChangedEventArgs(nameof(IsThisMonth));
        private static readonly PropertyChangedEventArgs _isSelectedPropertyArgs = new PropertyChangedEventArgs(nameof(IsSelected));
        private static readonly PropertyChangedEventArgs _eventColorPropertyArgs = new PropertyChangedEventArgs(nameof(EventColor));
        private static readonly PropertyChangedEventArgs _selectedTextColorColorPropertyArgs = new PropertyChangedEventArgs(nameof(SelectedTextColor));
        private static readonly PropertyChangedEventArgs _otherMonthColorPropertyArgs = new PropertyChangedEventArgs(nameof(OtherMonthColor));
        private static readonly PropertyChangedEventArgs _deselectedTextColorPropertyArgs = new PropertyChangedEventArgs(nameof(DeselectedTextColor));
        private static readonly PropertyChangedEventArgs _selectedBackgroundColorPropertyArgs = new PropertyChangedEventArgs(nameof(SelectedBackgroundColor));
        private static readonly PropertyChangedEventArgs _deselectedBackgroundColorPropertyArgs = new PropertyChangedEventArgs(nameof(DeselectedBackgroundColor));
        private static readonly PropertyChangedEventArgs _textColorPropertyArgs = new PropertyChangedEventArgs(nameof(TextColor));
        private static readonly PropertyChangedEventArgs _backgroundColorPropertyArgs = new PropertyChangedEventArgs(nameof(BackgroundColor));

        private DateTime _date;
        private bool _hasEvents;
        private bool _isThisMonth;
        private bool _isSelected;
        private Color _selectedTextColor = Color.White;
        private Color _otherMonthColor = Color.Silver;
        private Color _deselectedTextColor = Color.Default;
        private Color _selectedBackgroundColor = Color.FromHex("#2196F3");
        private Color _deselectedBackgroundColor = Color.Transparent;
        private Color _eventIndicatorColor = Color.FromHex("#FF4081");
        private Color _eventIndicatorSelectedColor = Color.FromHex("#FF4081");

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value, _datePropertyArgs);
        }

        public bool HasEvents
        {
            get => _hasEvents;
            set => SetProperty(ref _hasEvents, value, _hasEventsPropertyArgs);
        }

        public bool IsThisMonth
        {
            get => _isThisMonth;
            set => SetProperty(ref _isThisMonth, value, _isThisMonthPropertyArgs, _textColorPropertyArgs);
        }
        
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value, _isSelectedPropertyArgs, _textColorPropertyArgs, _backgroundColorPropertyArgs, _eventColorPropertyArgs);
        }

        public Color SelectedTextColor
        {
            get => _selectedTextColor;
            set => SetProperty(ref _selectedTextColor, value, _selectedTextColorColorPropertyArgs, _textColorPropertyArgs);
        }

        public Color OtherMonthColor
        {
            get => _otherMonthColor;
            set => SetProperty(ref _otherMonthColor, value, _otherMonthColorPropertyArgs, _textColorPropertyArgs);
        }

        public Color DeselectedTextColor
        {
            get => _deselectedTextColor;
            set => SetProperty(ref _deselectedTextColor, value, _deselectedTextColorPropertyArgs, _textColorPropertyArgs);
        }

        public Color SelectedBackgroundColor
        {
            get => _selectedBackgroundColor;
            set => SetProperty(ref _selectedBackgroundColor, value, _selectedBackgroundColorPropertyArgs, _backgroundColorPropertyArgs);
        }

        public Color DeselectedBackgroundColor
        {
            get => _deselectedBackgroundColor;
            set => SetProperty(ref _deselectedBackgroundColor, value, _deselectedBackgroundColorPropertyArgs, _backgroundColorPropertyArgs);
        }

        public Color EventIndicatorColor
        {
            get => _eventIndicatorColor;
            set => SetProperty(ref _eventIndicatorColor, value, _eventColorPropertyArgs);
        }

        public Color EventIndicatorSelectedColor
        {
            get => _eventIndicatorSelectedColor;
            set => SetProperty(ref _eventIndicatorSelectedColor, value, _eventColorPropertyArgs);
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

        private void SetProperty<TData>(
            ref TData storage,
            TData value,
            PropertyChangedEventArgs eventArgs,
            params PropertyChangedEventArgs[] alsoNotifyFor
            ) where TData : struct
        {
            if (storage.Equals(value))
                return;

            storage = value;

            PropertyChanged?.Invoke(this, eventArgs);

            foreach (var item in alsoNotifyFor)
                PropertyChanged?.Invoke(this, item);
        }
    }
}
