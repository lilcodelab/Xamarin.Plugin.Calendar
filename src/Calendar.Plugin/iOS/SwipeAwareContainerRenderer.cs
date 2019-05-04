using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.iOS;

[assembly: ExportRenderer(typeof(SwipeAwareContainer), typeof(SwipeAwareContainerRenderer))]
namespace Xamarin.Plugin.Calendar.iOS
{
    public class SwipeAwareContainerRenderer : ViewRenderer
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

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.AddGestureRecognizer(_rightGestureRecognizer);
                Control.AddGestureRecognizer(_leftGestureRecognizer);
                Control.AddGestureRecognizer(_upGestureRecognizer);
                Control.AddGestureRecognizer(_downGestureRecognizer);
            }

            if (e.OldElement != null)
            {
                Control.RemoveGestureRecognizer(_rightGestureRecognizer);
                Control.RemoveGestureRecognizer(_leftGestureRecognizer);
                Control.RemoveGestureRecognizer(_upGestureRecognizer);
                Control.RemoveGestureRecognizer(_downGestureRecognizer);
            }
        }

    }
}