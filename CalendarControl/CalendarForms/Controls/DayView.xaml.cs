using CalendarForms.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalendarForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentView
    {
        internal DayView()
        {
            InitializeComponent();
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (BindingContext is DayModel dayModel)
                dayModel.IsSelected = true;
        }
    }
}