using BalansirApp.Core.Products;
using BalansirApp.ViewModels.ItemReferences;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsListView_Page : ContentPage
    {
        private readonly ProductsList_ViewModel _viewModel;

        // CTOR
        public ProductsListView_Page()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = App.GetViewModel<ProductsList_ViewModel>();
        }

        // METHODS: Public
        public async void AddAct_Clicked(object sender, EventArgs e)
        {
            var product = (ProductView)((Button)sender).CommandParameter;
            await Navigation.PushAsync(new ActEdit_Page(product));
        }
        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductEdit_Page());
        }
        public async void EditProduct_Clicked(object sender, EventArgs e)
        {
            var item = (ProductView)((Button)sender).CommandParameter;
            var page = new ProductEdit_Page(item.Id);
            await Navigation.PushAsync(page);
        }
        public async void ShowProductActs_Clicked(object sender, EventArgs e)
        {
            var product = (ProductView)((Button)sender).CommandParameter;
            var page = _viewModel.GetActsPage(product);
            await Navigation.PushAsync(page);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            if (_viewModel.Items.Count == 0)
                _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}