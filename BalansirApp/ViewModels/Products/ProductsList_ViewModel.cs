using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.Pages;
using BalansirApp.ViewModels.Acts;
using BalansirApp.ViewModels.Common;
using Xamarin.Forms;

namespace BalansirApp.ViewModels.ItemReferences
{
    public class ProductsList_ViewModel : EntityList_ViewModel<Product, ProductView, ProductsQueryParam>
    {
        private string _searchName;

        // PROPS: Public Commands
        public Command ResetFilterCommand { get; }

        public string SearchName
        {
            get => _searchName;
            set
            {
                string oldValue = _searchName;
                _searchName = value;
                InformPropertyChanged(oldValue, _searchName);
            }
        }

        // PROPS: Public
        public override string Title => "СКЛАД";
        public override string ItemChanged_EventName => Consts.ProductsChanged_EventName;

        // CTOR
        public ProductsList_ViewModel(ISettings settings, IProductsService entityService)
            : base(settings, entityService)
        {
            this.ResetFilterCommand = new Command(() => ResetFilter());
        }

        // METHODS: Public
        public ActsListView_Page GetActsPage(ProductView productView)
        {
            var filterProps = ActListFilterProps.GetDefault(_settings);
            filterProps.ProductId = productView.Id;
            return new ActsListView_Page(filterProps);
        }

        protected override ProductsQueryParam GetQueryParam()
        {
            return new ProductsQueryParam(this.PageSize, this.CurrentPage, this.SearchName);
        }
        protected override void Subscribe_ItemChangedEvent()
        {
            MessagingCenter.Subscribe<ProductEdit_ViewModel>
                (this,
                this.ItemChanged_EventName,
                async (obj) =>
                {
                    await this.ExecuteLoadItemsCommand();
                }
            );

            MessagingCenter.Subscribe<ActEdit_ViewModel>
                (this,
                this.ItemChanged_EventName,
                async (obj) =>
                {
                    await this.ExecuteLoadItemsCommand();
                }
            );
        }

        void ResetFilter()
        {
            this.SearchName = null;
        }
    }
}