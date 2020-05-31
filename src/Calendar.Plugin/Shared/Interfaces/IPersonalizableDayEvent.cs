using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Interfaces
{
    public interface IPersonalizableDayEvent
    {
        #region PersonalizableProperties
        /// <summary>
        /// Color to use as indicator when there are events on the day
        /// if the EventIndicatorColor is null then the general EventIndicatorColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorColor { get; set; }

        /// <summary>
        /// Color to use as indicator when the day is selected and there are events on the day, if this is null then the EventIndicatorColor is used,
        /// if the EventIndicatorColor is null then the general EventIndicatorSelectedColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorSelectedColor { get; set; }

        /// <summary>
        /// Color for text to use as indicator when there are events on the day
        /// if the EventIndicatorTextColor is null then the general EventIndicatorTextColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorTextColor { get; set; }

        /// <summary>
        /// Color for text to use as indicator when the day is selected and there are events on the day, if this is null then the EventIndicatorTextColor is used,
        /// if the EventIndicatorSelectedTextColor is null then the general EventIndicatorSelectedTextColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorSelectedTextColor { get; set; }
        #endregion
    }
}
