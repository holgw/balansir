using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Components.Filter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UniversalSearchInput : ContentView
    {
        public static readonly BindableProperty SearchNameProperty = BindableProperty.Create(
            propertyName: "SearchName",
            returnType: typeof(String),
            declaringType: typeof(UniversalSearchInput),
            defaultValue: default(string));

        public string SearchName
        {
            get => (string)GetValue(SearchNameProperty);
            set => SetValue(SearchNameProperty, value);
        }

        // CTOR
        public UniversalSearchInput()
        {
            InitializeComponent();
        }

        async void ShowBarcodeReaderPage(object sender, EventArgs e)
        {
            var helper = new BarcodeScanHelper(this.Navigation);
            await helper.ScanBarcode((result) => this.SearchName = result);
        }
    }
}