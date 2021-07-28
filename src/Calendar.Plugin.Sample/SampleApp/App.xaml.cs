using SampleApp.Views;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
