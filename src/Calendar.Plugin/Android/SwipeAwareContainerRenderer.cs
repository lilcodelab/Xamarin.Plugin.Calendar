using System;
using Android.Content;
using Android.Views;
using Xamarin.Plugin.Calendar.Android;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(SwipeAwareContainer), typeof(SwipeAwareContainerRenderer))]
namespace Xamarin.Plugin.Calendar.Android
{
    internal class SwipeAwareContainerRenderer : ViewRenderer
    {
        private const int SwipeDistanceThreshold = 50;
        private const int SwipeVelocityThreshold = 20;
        private GestureDetector _gestureDetector;
        private GestureDetector.IOnGestureListener _gestureListener;
        private bool _isDisposed;

        public SwipeAwareContainerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                DisposeGestureDetectorAndListener();
            }

            if (e.NewElement != null)
            {
                _gestureListener = new InnerGestureListener(Element as SwipeAwareContainer);
                _gestureDetector = new GestureDetector(Context, _gestureListener);
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (_isDisposed)
                return false;

            if (Element is SwipeAwareContainer element && !element.SwipeDetectionDisabled)
                _gestureDetector?.OnTouchEvent(ev);

            return base.OnInterceptTouchEvent(ev);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (disposing)
            {
                DisposeGestureDetectorAndListener();
            }

            base.Dispose(disposing);
        }

        private void DisposeGestureDetectorAndListener()
        {
            _gestureDetector?.Dispose();
            _gestureDetector = null;

            _gestureListener?.Dispose();
            _gestureListener = null;
        }

        private class InnerGestureListener : GestureDetector.SimpleOnGestureListener
        {
            private SwipeAwareContainer SwipeAwareContainer { get; }

            public InnerGestureListener(SwipeAwareContainer swipeAwareContainer)
            {
                SwipeAwareContainer = swipeAwareContainer;
            }

            public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {
                if (SwipeAwareContainer == null)
                    return false;

                float distanceX = e2.GetX() - e1.GetX();
                float distanceY = e2.GetY() - e1.GetY();

                if (Math.Abs(distanceX) > Math.Abs(distanceY))
                {
                    if (Math.Abs(distanceX) > SwipeDistanceThreshold && Math.Abs(velocityX) > SwipeVelocityThreshold)
                    {
                        if (distanceX > 0)
                            SwipeAwareContainer.OnSwipeRight();
                        else
                            SwipeAwareContainer.OnSwipeLeft();

                        return true;
                    }
                }
                else
                {
                    if (Math.Abs(distanceY) > SwipeDistanceThreshold && Math.Abs(velocityY) > SwipeVelocityThreshold)
                    {
                        if (distanceY > 0)
                            SwipeAwareContainer.OnSwipeDown();
                        else
                            SwipeAwareContainer.OnSwipeUp();

                        return true;
                    }
                }

                return false;
            }
        }
    }
}