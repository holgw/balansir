using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About_Page : ContentPage
    {
        public string UsedComponents => 
            "Графика: ";

        public About_Page()
        {
            InitializeComponent();
        }
    }
}