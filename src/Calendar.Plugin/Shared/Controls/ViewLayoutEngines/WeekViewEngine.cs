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
    internal class WeekViewEngine : ViewLayoutBase, IViewLayoutEngine
    {
        private readonly int _numberOfWeeks;
        private readonly int _unitSizeinDays;

        public WeekViewEngine(CultureInfo culture, int numberOfWeeks) : base(culture)
        {
            _numberOfWeeks = numberOfWeeks;
            _unitSizeinDays = 7 * numberOfWeeks;
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
                _numberOfWeeks
            );

            return grid;
        }

        public DateTime GetFirstDate(DateTime dateToShow)
        {
            var firstWeekStart = GetFirstDateOfWeek(dateToShow);

            return firstWeekStart;
        }

        public DateTime GetNextUnit(DateTime forDate)
        {
            return forDate.AddDays(_unitSizeinDays);
        }

        public DateTime GetNextUnit(DateTime forDate, int numberOfUnits)
        {
            return forDate.AddDays(_unitSizeinDays * numberOfUnits);
        }

        public DateTime GetPreviousUnit(DateTime forDate)
        {
            return forDate.AddDays(_unitSizeinDays * -1);
        }

        public DateTime GetPreviousUnit(DateTime forDate, int numberOfUnits)
        {
            return forDate.AddDays(_unitSizeinDays * -1 * numberOfUnits);
        }
    }
}
