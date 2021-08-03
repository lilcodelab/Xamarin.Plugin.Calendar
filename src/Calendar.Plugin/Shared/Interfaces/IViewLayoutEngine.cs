using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;

namespace Xamarin.Plugin.Calendar.Shared.Interfaces
{
    internal interface IViewLayoutEngine
    {
        Grid GenerateLayout(
            List<DayView> dayViews,
            double daysTitleHeight,
            Color daysTitleColor,
            Style daysTitleLabelStyle,
            double dayViewSize,
            ICommand dayTappedCommand,
            PropertyChangedEventHandler dayModelPropertyChanged
        );

        DateTime GetFirstDate(DateTime dateToShow);

        DateTime GetNextUnit(DateTime forDate);

        DateTime GetNextUnit(DateTime forDate, int numberOfUnits);

        DateTime GetPreviousUnit(DateTime forDate);

        DateTime GetPreviousUnit(DateTime forDate, int numberOfUnits);
    }
}
