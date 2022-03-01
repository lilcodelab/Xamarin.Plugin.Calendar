using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Controls;
using Xamarin.Plugin.Calendar.Models;

namespace Xamarin.Plugin.Calendar.Shared.Controls.ViewLayoutEngines
{
    internal abstract class ViewLayoutBase
    {
        protected const int _numberOfDaysInWeek = 7;

        protected ViewLayoutBase(CultureInfo culture)
        {
            Culture = culture;
        }

        public CultureInfo Culture { get; set; }

        protected DateTime GetFirstDateOfWeek(DateTime dateInWeek)
        {
            var difference = (7 + (dateInWeek.DayOfWeek - Culture.DateTimeFormat.FirstDayOfWeek)) % 7;
            return dateInWeek.AddDays(-1 * difference).Date;
        }

        protected Grid GenerateWeekLayout(
                List<DayView> dayViews,
                object bindingContext,
                string daysTitleHeightBindingName,
                string daysTitleColorBindingName,
                string daysTitleLabelStyleBindingName,
                string dayViewSizeBindingName,
                ICommand dayTappedCommand,
                PropertyChangedEventHandler dayModelPropertyChanged,
                int numberOfWeeks
            )
        {
            var rowDefinition = new RowDefinition()
            {
                BindingContext = bindingContext,
            };
            rowDefinition.SetBinding(RowDefinition.HeightProperty, daysTitleHeightBindingName);

            var grid = new Grid
            {
                ColumnSpacing = 0d,
                RowDefinitions = new RowDefinitionCollection()
                {
                    rowDefinition,
                }
            };

            for (int i = 0; i < _numberOfDaysInWeek; i++)
            {
                var label = new Label()
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                label.BindingContext = bindingContext;
                label.SetBinding(Label.TextColorProperty, daysTitleColorBindingName);
                label.SetBinding(Label.StyleProperty, daysTitleLabelStyleBindingName);

                Grid.SetRow(label, 0);
                Grid.SetColumn(label, i);

                grid.Children.Add(label);
            }

            dayViews.Clear();

            for (int i = 1; i <= numberOfWeeks; i++)
            {
                rowDefinition = new RowDefinition()
                {
                    BindingContext = bindingContext,
                };
                rowDefinition.SetBinding(RowDefinition.HeightProperty, dayViewSizeBindingName);
                grid.RowDefinitions.Add(rowDefinition);

                for (int ii = 0; ii < 7; ii++)
                {
                    var dayView = new DayView();
                    var dayModel = new DayModel();

                    dayView.BindingContext = dayModel;
                    dayModel.DayTappedCommand = dayTappedCommand;
                    dayModel.PropertyChanged += dayModelPropertyChanged;

                    Grid.SetRow(dayView, i);
                    Grid.SetColumn(dayView, ii);

                    dayViews.Add(dayView);
                    grid.Children.Add(dayView);
                }
            }

            return grid;
        }
    }
}
