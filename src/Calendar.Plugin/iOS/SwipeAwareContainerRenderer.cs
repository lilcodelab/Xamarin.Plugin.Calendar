using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.iOS;

[assembly: ExportRenderer(typeof(SwipeAwareContainer), typeof(SwipeAwareContainerRenderer))]
namespace Xamarin.Plugin.Calendar.iOS
{
    public class SwipeAwareContainerRenderer : VisualElementRenderer<ContentView>
    {
        private readonly UISwipeGestureRecognizer _rightGestureRecognizer;
        private readonly UISwipeGestureRecognizer _leftGestureRecognizer;
        private readonly UISwipeGestureRecognizer _upGestureRecognizer;
        private readonly UISwipeGestureRecognizer _downGestureRecognizer;

        public SwipeAwareContainerRenderer()
        {
            _rightGestureRecognizer = new UISwipeGestureRecognizer(() => (Element as SwipeAwareContainer)?.OnSwipeRight()) { Direction = UISwipeGestureRecognizerDirection.Right };
            _leftGestureRecognizer = new UISwipeGestureRecognizer(() => (Element as SwipeAwareContainer)?.OnSwipeLeft()) { Direction = UISwipeGestureRecognizerDirection.Left };
            _upGestureRecognizer = new UISwipeGestureRecognizer(() => (Element as SwipeAwareContainer)?.OnSwipeUp()) { Direction = UISwipeGestureRecognizerDirection.Up };
            _downGestureRecognizer = new UISwipeGestureRecognizer(() => (Element as SwipeAwareContainer)?.OnSwipeDown()) { Direction = UISwipeGestureRecognizerDirection.Down};
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (!(Element is SwipeAwareContainer element))
                return;

            switch (e.PropertyName)
            {
                case nameof(SwipeAwareContainer.SwipeDetectionDisabled):
                    if (element.SwipeDetectionDisabled)
                        RemoveGestureRecognizers();
                    else
                        AddGestureRecognizers();
                    break;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (NativeView == null)
                return;

            if (e.NewElement != null)
                AddGestureRecognizers();

            if (e.OldElement != null)
                RemoveGestureRecognizers();
        }

        private void AddGestureRecognizers()
        {
            try
            {
                if (!NativeView.GestureRecognizers.Contains(_rightGestureRecognizer))
                    NativeView.AddGestureRecognizer(_rightGestureRecognizer);

                if (!NativeView.GestureRecognizers.Contains(_leftGestureRecognizer))
                    NativeView.AddGestureRecognizer(_leftGestureRecognizer);

                if (!NativeView.GestureRecognizers.Contains(_upGestureRecognizer))
                    NativeView.AddGestureRecognizer(_upGestureRecognizer);

                if (!NativeView.GestureRecognizers.Contains(_downGestureRecognizer))
                    NativeView.AddGestureRecognizer(_downGestureRecognizer);
            }
            catch (Exception)
            { }
        }

        private void RemoveGestureRecognizers()
        {
            try
            {
                if (NativeView.GestureRecognizers.Contains(_rightGestureRecognizer))
                    NativeView.RemoveGestureRecognizer(_rightGestureRecognizer);

                if (NativeView.GestureRecognizers.Contains(_leftGestureRecognizer))
                    NativeView.RemoveGestureRecognizer(_leftGestureRecognizer);

                if (NativeView.GestureRecognizers.Contains(_upGestureRecognizer))
                    NativeView.RemoveGestureRecognizer(_upGestureRecognizer);

                if (NativeView.GestureRecognizers.Contains(_downGestureRecognizer))
                    NativeView.RemoveGestureRecognizer(_downGestureRecognizer);
            }
            catch (Exception)
            { }
        }

    }
}