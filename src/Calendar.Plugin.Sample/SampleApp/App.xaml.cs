using SampleApp.Views;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainTabbedPage());
        }
    }
}
