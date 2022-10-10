using BalansirApp.Core.Acts;
using BalansirApp.ViewModels.Acts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActsListView_Page : ContentPage
    {
        private readonly ActsList_ViewModel _viewModel;

        // CTOR
        public ActsListView_Page()
        {
            InitializeComponent();

            this.BindingContext = _viewModel = App.GetViewModel<ActsList_ViewModel>();
            _viewModel.Setup();

            // Если журнал актов открывается не для конкретного продукта,
            // то кнопку создания актов нужно скрыть
            this.ToolbarItems.Clear();
        }
        public ActsListView_Page(ActListFilterProps filterProps)
        {
            InitializeComponent();

            this.BindingContext = _viewModel = App.GetViewModel<ActsList_ViewModel>();
            _viewModel.Setup(filterProps);

            // Если журнал актов открывается не для конкретного продукта,
            // то кнопку создания актов нужно скрыть
            if (!filterProps.ProductId.HasValue)
                this.ToolbarItems.Clear();
        }

        // BUTTONS
        public async void AddItem_Clicked(object sender, EventArgs e)
        {
            var page = _viewModel.GetNewItemPage();
            await Navigation.PushAsync(page);
        }
        public async void Show_EditActPage(object sender, EventArgs e)
        {
            var item = (ActView)((Button)sender).CommandParameter;
            var page = _viewModel.GetEditItemPage(item.Id);
            await Navigation.PushAsync(page);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            _viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
