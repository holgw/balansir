using BalansirApp.ViewModels.Common;
using Xamarin.Forms;

namespace BalansirApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.BindingContext = App.GetViewModel<Shell_ViewModel>();
        }
    }
}
