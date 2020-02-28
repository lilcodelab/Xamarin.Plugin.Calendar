﻿using Xamarin.Plugin.Calendar.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Internal class used by Xamarin.Plugin.Calendar
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        #region Bindable properties

        /// <summary> Bindable property for DayViewSize </summary>
        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(DayView), 40.0);

        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        /// <summary> Bindable property for DayViewCornerRadius </summary>
        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(DayView), 20f);

        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        /// <summary> Bindable property for DaysLabelStyle </summary>
        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(DayView), null);

        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        #endregion

        #region Bindable personalizable actions
        public static readonly BindableProperty TappedDayCommandProperty =
            BindableProperty.Create(nameof(TappedDayCommand), typeof(Command<DateTime>), typeof(Calendar));
        public Command<DateTime> TappedDayCommand
        {
            get => (Command<DateTime>) GetValue(TappedDayCommandProperty);
            set => SetValue(TappedDayCommandProperty, value);
        }
        #endregion

        internal DayView()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (BindingContext is DayModel dayModel && !dayModel.IsDisabled)
            {
                dayModel.IsSelected = true;
                TappedDayCommand?.Execute(dayModel.Date);
            }
        }
    }
}