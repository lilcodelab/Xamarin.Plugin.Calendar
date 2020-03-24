using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Controls
{
    internal class DataTemplateView : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(DataTemplateView), null, propertyChanged: OnItemTemplateChanged);

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        internal DataTemplateView() { }

        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DataTemplateView view)
                view.CreateContent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            CreateContent();
        }

        private void CreateContent()
        {
            if (BindingContext == null)
                return;

            var itemContent = ItemTemplate?.CreateContent(BindingContext);

            Content = itemContent as View ?? (itemContent as ViewCell)?.View;
        }
    }
}
