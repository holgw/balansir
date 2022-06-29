using BalansirApp.ViewModels.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings_Page : ContentPage
    {
        // CTOR
        public Settings_Page()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<SettingsEdit_ViewModel>();
        }
    }
}