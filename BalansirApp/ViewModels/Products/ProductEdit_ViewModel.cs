using BalansirApp.Core.Products;
using BalansirApp.Core.Products.DataAccess;
using BalansirApp.ViewModels.Common;
using Xamarin.Forms;

namespace BalansirApp.ViewModels.Products
{
    public class ProductEdit_ViewModel :
        EntityEdit_ViewModel<Product, ProductView, ProductsQueryParam>
    {
        public override string Title => _source == null ? "НОВЫЙ ПРОДУКТ" : "ПРОДУКТ";

        // PROPS: Input
        [FormField]
        public string Name
        {
            get => _source?.Name;
            set
            {
                string oldValue = _source.Name;
                _source.Name = value;
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField]
        public string Code
        {
            get => _source?.Code;
            set
            {
                string oldValue = _source.Code;
                _source.Code = value;
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField]
        public string Units
        {
            get => _source?.Units;
            set
            {
                string oldValue = _source.Units;
                _source.Units = value;
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField]
        public string Description
        {
            get => _source?.Description;
            set
            {
                string oldValue = _source.Description;
                _source.Description = value;
                InformPropertyChanged(oldValue, value);
            }
        }

        // CTOR
        public ProductEdit_ViewModel(IProductsService entityService)
            : base(entityService)
        {
        }

        // METHODS: Protected
        protected override ProductView BuildNewEntityView()
        {
            var newEntityView = base.BuildNewEntityView();
            newEntityView.Units = "шт.";
            return newEntityView;
        }
        protected override void NotifyParents()
        {
            MessagingCenter.Send(this, Consts.ProductsChanged_EventName);
        }
    }
}
