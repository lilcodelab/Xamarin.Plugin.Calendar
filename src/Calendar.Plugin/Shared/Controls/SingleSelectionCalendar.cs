using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Interfaces;

namespace Xamarin.Plugin.Calendar.Controls
{
    public class SingleSelectionCalendar : Calendar
    {
        public SingleSelectionCalendar()
        {
            monthDaysView.SelectionType = Enums.SelectionType.Day;
        }
    }
}