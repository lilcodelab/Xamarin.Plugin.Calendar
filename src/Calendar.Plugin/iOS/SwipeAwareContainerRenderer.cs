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

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            var control = NativeView;

            if (control == null)
                return;

            if (e.NewElement != null)
            {
                control.AddGestureRecognizer(_rightGestureRecognizer);
                control.AddGestureRecognizer(_leftGestureRecognizer);
                control.AddGestureRecognizer(_upGestureRecognizer);
                control.AddGestureRecognizer(_downGestureRecognizer);
            }

            if (e.OldElement != null)
            {
                control.RemoveGestureRecognizer(_rightGestureRecognizer);
                control.RemoveGestureRecognizer(_leftGestureRecognizer);
                control.RemoveGestureRecognizer(_upGestureRecognizer);
                control.RemoveGestureRecognizer(_downGestureRecognizer);
            }
        }
    }
}