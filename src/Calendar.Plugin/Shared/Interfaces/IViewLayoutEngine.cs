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
            object bindingContext,
            string daysTitleHeightBindingName,
            string daysTitleColorBindingName,
            string daysTitleLabelStyleBindingName,
            string dayViewSizeBindingName,
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
