using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Shared.Models
{
    /// <summary>
    /// Wrapper to allow change the dot color
    /// </summary>
    public class DayEventCollection<T> : List<T>
    {
        /// <summary>
        /// Empty contructor extends from base()
        /// </summary>
        public DayEventCollection() : base()
        {

        }

        /// <summary>
        /// Color contructor extends from base()
        /// </summary>
        /// <param name="eventIndicatorColor"></param>
        /// <param name="eventIndicatorSelectedColor"></param>
        public DayEventCollection(Color? eventIndicatorColor, Color? eventIndicatorSelectedColor) : base()
        {
            EventIndicatorColor = eventIndicatorColor;
            EventIndicatorSelectedColor = eventIndicatorSelectedColor;
        }

        /// <summary>
        /// IEnumerable contructor extends from base(IEnumerable collection)
        /// </summary>
        /// <param name="collection"></param>
        public DayEventCollection(IEnumerable<T> collection) : base(collection)
        {

        }

        /// <summary>
        /// Capacity contructor extends from base(int capacity)
        /// </summary>
        /// <param name="capacity"></param>
        public DayEventCollection(int capacity) : base(capacity)
        {

        }

        #region PersonalizableProperties
        /// <summary>
        /// Color to use in the dot when there are events on the day
        /// if the EventIndicatorColor is null then the general EventIndicatorColor of the Calendar will be used
        /// </summary>
        public Color? EventIndicatorColor { get; set; }

        /// <summary>
        /// Color to use in the dot when the day is selected and there are events on the day if this is null then the EventIndicator color is used,
        /// if the EventIndicatorColor is null then the general EventIndicatorSelectedColor of the Calendar will be used
        /// </summary>
        public Color? EventIndicatorSelectedColor { get; set; }
        #endregion

        #region Implemented
        #endregion
    }
}
