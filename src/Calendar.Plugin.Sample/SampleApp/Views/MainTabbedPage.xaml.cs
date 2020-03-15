using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
            AndroidSpecific.TabbedPage.SetToolbarPlacement(this, AndroidSpecific.ToolbarPlacement.Bottom);
            AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}