using BalansirApp.Components;
using BalansirApp.ViewModels.Products;
using System;
using Xamarin.Forms;

namespace BalansirApp.Pages
{
    public partial class ProductEdit_Page : ContentPage
    {
        private readonly ProductEdit_ViewModel _viewModel;

        // CTOR
        public ProductEdit_Page(int? id = null)
        {
            InitializeComponent();

            this.BindingContext = _viewModel = App.GetViewModel<ProductEdit_ViewModel>();
            _viewModel.Setup(id);
        }

        // METHODS: Public
        public async void DeleteClicked(object sender, EventArgs e)
        {
            string qmsg = "Вы уверены, что хотите удалить выбранный продукт и все связанные с ним акты?";
            bool result = await DisplayAlert("Предупреждение", qmsg, "Да", "Нет");

            if (result)
            {
                var param = ((Button)sender).CommandParameter;
                _viewModel.DeleteCommand.Execute(param);
            }
        }
        public async void ShowBarcodeReaderPage(object sender, EventArgs e)
        {
            var helper = new BarcodeScanHelper(this.Navigation);
            await helper.ScanBarcode((result) => _viewModel.Code = result);
        }
    }
}