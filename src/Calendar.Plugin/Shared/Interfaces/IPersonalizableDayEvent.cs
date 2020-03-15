using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Interfaces
{
    public interface IPersonalizableDayEvent
    {
        #region PersonalizableProperties
        /// <summary>
        /// Color to use in the dot when there are events on the day
        /// if the EventIndicatorColor is null then the general EventIndicatorColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorColor { get; set; }

        /// <summary>
        /// Color to use in the dot when the day is selected and there are events on the day if this is null then the EventIndicator color is used,
        /// if the EventIndicatorColor is null then the general EventIndicatorSelectedColor of the Calendar will be used
        /// </summary>
        Color? EventIndicatorSelectedColor { get; set; }
        #endregion
    }
}
