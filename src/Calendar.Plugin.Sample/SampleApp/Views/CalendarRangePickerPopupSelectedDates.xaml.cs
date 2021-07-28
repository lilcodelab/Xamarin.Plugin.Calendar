using Rg.Plugins.Popup.Pages;
using SampleApp.Model;
using SampleApp.ViewModels;
using System;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarRangePickerPopupSelectedDates : PopupPage
    {
        private readonly Action<CalendarRangePickerResult> _onClosedPopup;

        public CalendarRangePickerPopupSelectedDates(Action<CalendarRangePickerResult> onClosedPopup)
        {
            _onClosedPopup = onClosedPopup;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CalendarRangePickerPopupSelectedDatesViewModel vm)
                vm.Closed += _onClosedPopup;
        }

        protected override void OnDisappearing()
        {
            if (BindingContext is CalendarRangePickerPopupSelectedDatesViewModel vm)
                vm.Closed -= _onClosedPopup;

            base.OnDisappearing();
        }
    }
}