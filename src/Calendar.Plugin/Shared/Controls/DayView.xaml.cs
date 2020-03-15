using Xamarin.Plugin.Calendar.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Xamarin.Plugin.Calendar.Controls
{
    /// <summary>
    /// Internal class used by Xamarin.Plugin.Calendar
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
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
                dayModel.DayTappedCommand?.Execute(dayModel.Date);
            }
        }
    }
}