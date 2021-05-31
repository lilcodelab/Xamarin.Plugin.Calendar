using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Xamarin.Plugin.Calendar.Controls
{
    public class RangeSelectionCalendar : Calendar
    {
        public RangeSelectionCalendar() : base()
        {
            monthDaysView.CurrentSelectionEngine = new RangedSelectionEngine();
        }
    }
}