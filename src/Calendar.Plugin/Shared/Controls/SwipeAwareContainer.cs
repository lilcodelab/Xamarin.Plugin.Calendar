using System;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Controls
{
    public class SwipeAwareContainer : ContentView
    {
        public static readonly BindableProperty SwipeDetectionDisabledProperty =
          BindableProperty.Create(nameof(SwipeDetectionDisabled), typeof(bool), typeof(SwipeAwareContainer), false);

        public bool SwipeDetectionDisabled
        {
            get => (bool)GetValue(SwipeDetectionDisabledProperty);
            set => SetValue(SwipeDetectionDisabledProperty, value);
        }

        public SwipeAwareContainer() : base()
        { }

        public event EventHandler SwipedLeft;
        public event EventHandler SwipedRight;
        public event EventHandler SwipedUp;
        public event EventHandler SwipedDown;

        public void OnSwipeLeft() => SwipedLeft?.Invoke(this, EventArgs.Empty);
        public void OnSwipeRight() => SwipedRight?.Invoke(this, EventArgs.Empty);
        public void OnSwipeUp() => SwipedUp?.Invoke(this, EventArgs.Empty);
        public void OnSwipeDown() => SwipedDown?.Invoke(this, EventArgs.Empty);
    }
}
