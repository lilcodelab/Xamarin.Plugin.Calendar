using System;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Controls
{
    public class SwipeAwareContainer: ContentView
    {
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
