using Xamarin.Plugin.Calendar.Controls.SelectionEngines;

namespace Xamarin.Plugin.Calendar.Controls
{
    public class MultiSelectionCalendar : Calendar
    {
        private readonly MultiSelectionEngine _multiSelectionEngine;

        public MultiSelectionCalendar()
        {
            monthDaysView.CurrentSelectionEngine = _multiSelectionEngine = new MultiSelectionEngine();
        }
    }
}