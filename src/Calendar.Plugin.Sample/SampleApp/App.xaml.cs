using Xamarin.Forms;

namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MyDemoPage());
            //MainPage = new NavigationPage(new MainPage());
        }
    }
}
