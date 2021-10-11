using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.Shared.Interfaces;

namespace Xamarin.Plugin.Calendar.Shared.Controls.ViewLayoutEngines
{
    internal class MonthViewEngine : ViewLayoutBase, IViewLayoutEngine
    {
        private const int _monthNumberOfWeeks = 6;

        public MonthViewEngine(CultureInfo culture) : base(culture)
        {
        }

        public Grid GenerateLayout(
            List<DayView> dayViews,
            double daysTitleHeight,
            Color daysTitleColor,
            Style daysTitleLabelStyle,
            double dayViewSize,
            ICommand dayTappedCommand,
            PropertyChangedEventHandler dayModelPropertyChanged
        )
        {
            var grid = GenerateWeekLayout(
                dayViews,
                daysTitleHeight,
                daysTitleColor,
                daysTitleLabelStyle,
                dayViewSize,
                dayTappedCommand,
                dayModelPropertyChanged,
                _monthNumberOfWeeks
            );

            return grid;
        }

        public DateTime GetFirstDate(DateTime dateToShow)
        {
            var firstOfMonth = new DateTime(dateToShow.Year, dateToShow.Month, 1);
            var firstWeekStart = GetFirstDateOfWeek(firstOfMonth);
            return firstWeekStart;
        }

        public DateTime GetNextUnit(DateTime forDate)
        {
            return forDate.AddMonths(1);
        }

        public DateTime GetNextUnit(DateTime forDate, int numberOfUnits)
        {
            return forDate.AddMonths(numberOfUnits);
        }

        public DateTime GetPreviousUnit(DateTime forDate)
        {
            return forDate.AddMonths(-1);
        }

        public DateTime GetPreviousUnit(DateTime forDate, int numberOfUnits)
        {
            return forDate.AddMonths(numberOfUnits * -1);
        }
    }
}
