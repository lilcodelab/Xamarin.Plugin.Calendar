using System;
using Android.Content;
using Android.Views;
using Xamarin.Plugin.Calendar.Android;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeAwareContainer), typeof(SwipeAwareContainerRenderer))]
namespace Xamarin.Plugin.Calendar.Android
{
    public class SwipeAwareContainerRenderer : ViewRenderer, GestureDetector.IOnGestureListener
    {
        private const int SwipeDistanceThreshold = 50;
        private const int SwipeVelocityThreshold = 20;
        private readonly GestureDetector _gestureDetector;

        public SwipeAwareContainerRenderer(Context context) : base(context)
        {
            _gestureDetector = new GestureDetector(context, this);
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (Element is SwipeAwareContainer element && !element.SwipeDetectionDisabled)
                _gestureDetector.OnTouchEvent(ev);

            return base.OnInterceptTouchEvent(ev);
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            if (!(Element is SwipeAwareContainer element))
                return false;

            float distanceX = e2.GetX() - e1.GetX();
            float distanceY = e2.GetY() - e1.GetY();

            if (Math.Abs(distanceX) > Math.Abs(distanceY))
            {
                if (Math.Abs(distanceX) > SwipeDistanceThreshold && Math.Abs(velocityX) > SwipeVelocityThreshold)
                {
                    if (distanceX > 0)
                        element.OnSwipeRight();
                    else
                        element.OnSwipeLeft();

                    return true;
                }
            }
            else
            {
                if (Math.Abs(distanceY) > SwipeDistanceThreshold && Math.Abs(velocityY) > SwipeVelocityThreshold)
                {
                    if (distanceY > 0)
                        element.OnSwipeDown();
                    else
                        element.OnSwipeUp();

                    return true;
                }
            }

            return false;
        }

        #region Unused gestures

        public bool OnDown(MotionEvent e) => false;
        public void OnLongPress(MotionEvent e) { }
        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) => false;
        public void OnShowPress(MotionEvent e) { }
        public bool OnSingleTapUp(MotionEvent e) => false;

        #endregion

    }
}