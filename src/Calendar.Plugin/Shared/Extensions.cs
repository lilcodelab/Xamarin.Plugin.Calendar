using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar
{
    internal static class Extensions
    {
        internal static string Capitalize(this string source)
        {
            if (source.Length == 0)
                return source;

            return char.ToUpper(source[0]) + source.Substring(1, source.Length - 1);
        }

        internal static object CreateContent(this DataTemplate dataTemplate, object itemModel)
        {
            if (dataTemplate is DataTemplateSelector templateSelector)
            {
                var template = templateSelector.SelectTemplate(itemModel, null);
                template.SetValue(BindableObject.BindingContextProperty, itemModel);

                return template.CreateContent();
            }

            dataTemplate.SetValue(BindableObject.BindingContextProperty, itemModel);
            return dataTemplate.CreateContent();
        }
    }
}
