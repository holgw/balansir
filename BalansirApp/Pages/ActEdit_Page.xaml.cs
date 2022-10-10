using BalansirApp.Core.Acts;
using BalansirApp.Core.Products;
using BalansirApp.ViewModels.Acts;
using System;
using Xamarin.Forms;

namespace BalansirApp.Pages
{
    public partial class ActEdit_Page : ContentPage
    {
        private readonly ActEdit_ViewModel _viewModel;

        // CTOR
        public ActEdit_Page(int actId)
        {
            InitializeComponent();
            BindingContext = _viewModel = App.GetViewModel<ActEdit_ViewModel>();
            _viewModel.Setup(actId);
        }
        public ActEdit_Page(ProductView product)
        {
            InitializeComponent();
            BindingContext = _viewModel = App.GetViewModel<ActEdit_ViewModel>();
            var newActView = ActView.GetNew(product);
            _viewModel.Setup(null, newActView);
        }

        // METHODS: Private
        public void incrementHoursButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IncrementHours();
        }
        public void decrementHoursButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.DecrementHours();
        }
        public void incrementMinutesButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IncrementMinutes();
        }
        public void decrementMinutesButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.DecrementMinutes();
        }
        public void incrementDeltaButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.IncrementDelta();
        }
        public void decrementDeltaButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.DecrementDelta();
        }
        public async void DeleteClicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Предупреждение", "Вы уверены, что хотите удалить выбранный акт?", "Да", "Нет");

            if (result)
            {
                var param = ((Button)sender).CommandParameter;
                _viewModel.DeleteCommand.Execute(param);
            }
        }
    }
}