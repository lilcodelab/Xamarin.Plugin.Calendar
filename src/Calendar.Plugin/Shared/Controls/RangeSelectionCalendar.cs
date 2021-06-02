
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Ranged selection version of the calendar control
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class RangeSelectionCalendar : Calendar
    {
        /// <summary> Bindable property for StartDate </summary>
        public static readonly BindableProperty StartDateProperty =
          BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(RangeSelectionCalendar), DateTime.Today, propertyChanged: OnStartDateChanged);

        private static void OnStartDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RangeSelectionCalendar)bindable)._selectionEngine.SelectDateRange((DateTime)newValue);
        }

        /// <summary> Beggining of selected interval </summary>
        public DateTime StartDate
        {
            get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        /// <summary> Bindable property for EndDate </summary>
        public static readonly BindableProperty EndDateProperty =
          BindableProperty.Create(nameof(EndDate), typeof(DateTime), typeof(RangeSelectionCalendar), DateTime.Today, propertyChanged: OnEndDateChanged);

        private static void OnEndDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RangeSelectionCalendar)bindable)._selectionEngine.SelectDateRange((DateTime)newValue);
        }

        /// <summary> End of selected interval </summary>
        public DateTime EndDate
        {
            get => (DateTime)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RangeSelectionCalendar() : base()
        {
            monthDaysView.CurrentSelectionEngine = new RangedSelectionEngine();
            _selectionEngine = monthDaysView.CurrentSelectionEngine as RangedSelectionEngine;
        }

        private readonly RangedSelectionEngine _selectionEngine;
    }
}
