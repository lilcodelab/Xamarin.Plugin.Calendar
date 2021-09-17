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
                double daysTitleHeight,
                Color daysTitleColor,
                Style daysTitleLabelStyle,
                double dayViewSize,
                ICommand dayTappedCommand,
                PropertyChangedEventHandler dayModelPropertyChanged,
                int numberOfWeeks
            )
        {
            var grid = new Grid
            {
                ColumnSpacing = 0d,
                RowDefinitions = new RowDefinitionCollection()
                {
                    new RowDefinition(){
                        Height = daysTitleHeight
                    },
                }
            };

            for (int i = 0; i < _numberOfDaysInWeek; i++)
            {
                var label = new Label()
                {
                    TextColor = daysTitleColor,
                    Style = daysTitleLabelStyle,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, i);

                grid.Children.Add(label);
            }

            dayViews.Clear();

            for (int i = 1; i <= numberOfWeeks; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = dayViewSize,
                });

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
