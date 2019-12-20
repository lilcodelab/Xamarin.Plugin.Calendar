using Xamarin.Plugin.Calendar.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.Calendar.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        #region Bindable properties

        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(DayView), 40.0);

        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(DayView), 20f);

        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(DayView), null);

        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        #endregion

        internal DayView()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Bindable property for DayTapped
        /// </summary>
        public static readonly BindableProperty DayTappedProperty =
            BindableProperty.Create(nameof(DayTapped), typeof(Action<DateTime>), typeof(DayView), null);

        /// <summary>
        /// Action to run after a day has been tapped.
        /// </summary>
        public Action<DateTime> DayTapped
        {
            get => (Action<DateTime>) GetValue(DayTappedProperty);
            set => SetValue(DayTappedProperty, value);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (BindingContext is DayModel dayModel)
            {
                dayModel.IsSelected = true;
                DayTapped?.Invoke(dayModel.Date);
            }
        }
    }
}