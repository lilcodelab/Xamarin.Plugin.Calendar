
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
          BindableProperty.Create(nameof(StartDate), typeof(DateTime?), typeof(RangeSelectionCalendar), null, BindingMode.TwoWay, propertyChanged: OnStartDateChanged);

        private static void OnStartDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (((RangeSelectionCalendar)bindable)._isRangeSelection == false)
               ((RangeSelectionCalendar)bindable)._selectionEngine.SelectDateRange((DateTime?)newValue);
        }

        /// <summary> Beggining of selected interval </summary>
        public DateTime? StartDate
        {
            get => (DateTime?)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        /// <summary> Bindable property for EndDate </summary>
        public static readonly BindableProperty EndDateProperty =
          BindableProperty.Create(nameof(EndDate), typeof(DateTime?), typeof(RangeSelectionCalendar), null, BindingMode.TwoWay, propertyChanged: OnEndDateChanged);

        private static void OnEndDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(((RangeSelectionCalendar)bindable)._isRangeSelection == false)
                ((RangeSelectionCalendar)bindable)._selectionEngine.SelectDateRange((DateTime?)newValue);
            ((RangeSelectionCalendar)bindable)._isRangeSelection = false;
        }

        /// <summary> End of selected interval </summary>
        public DateTime? EndDate
        {
            get => (DateTime?)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        private bool _isRangeSelection = false;

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if(propertyName == nameof(SelectedDates))
            {
                var first = _selectionEngine.GetDateRange();
                
                if (first.Count > 0)
                {
                    _isRangeSelection = true;
                    SetValue(StartDateProperty, first.First());
                    SetValue(EndDateProperty, first.Last());
                }
            }
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
