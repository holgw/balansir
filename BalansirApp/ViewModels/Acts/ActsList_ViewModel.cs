using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.Core.Products;
using BalansirApp.Pages;
using BalansirApp.ViewModels.Common;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BalansirApp.ViewModels.Acts
{
    public class ActsList_ViewModel : EntityList_ViewModel<Act, ActView, ActsQueryParam>
    {
        private readonly IProductsService _productsService;

        private DateTime _start;
        private DateTime _end;

        private int? _productId;
        private ProductView _product;
        private bool _isProductLabelVisible;

        // PROPS: Public Commands
        public Command ResetFilterCommand { get; }

        // PROPS - FILTER: Public
        public ProductView Product
        {
            get => _product;
            set => this.SetProperty(ref _product, value);
        }
        public bool IsProductLableVisible
        {
            get => _isProductLabelVisible;
            set => this.SetProperty(ref _isProductLabelVisible, value);
        }
        public DateTime Start
        {
            get => _start;
            set => this.SetProperty(ref _start, value);
        }
        public DateTime End
        {
            get => _end;
            set => this.SetProperty(ref _end, value);
        }
        public int? ProductId
        {
            get => _productId;
            set
            {
                int? oldValue = _productId;
                _productId = value;
                InformPropertyChanged(oldValue, _productId);
            }
        }

        public override string Title => "ПРИХОД И РАСХОД";
        public override string ItemChanged_EventName => Consts.ActsChanged_EventName;

        // CTOR
        public ActsList_ViewModel(
            ISettingsProvider settings,
            IActsService entityService,
            IProductsService productsService)
            : base(settings, entityService)
        {
            _productsService = productsService ?? throw new ArgumentNullException(nameof(productsService));

            this.ResetFilterCommand = new Command(() => ResetFilter());
        }

        // METHODS: Public
        public void Setup(ActListFilterProps? filterProps = null)
        {
            if (filterProps == null)
            {
                filterProps = ActListFilterProps.GetDefault(_settings);
            }

            this.Start = filterProps.Value.Start;
            this.End = filterProps.Value.End;
            this.ProductId = filterProps.Value.ProductId;


        }
        public ActEdit_Page GetNewItemPage()
        {
            return new ActEdit_Page(this.Product);
        }
        public ActEdit_Page GetEditItemPage(int actId)
        {
            return new ActEdit_Page(actId);
        }

        protected override ActsQueryParam GetQueryParam()
        {
            return new ActsQueryParam(this.PageSize, this.CurrentPage, this.ProductId, this.Start, this.End);
        }
        protected override void Subscribe_ItemChangedEvent()
        {
            MessagingCenter.Subscribe<ActEdit_ViewModel>
                (this,
                this.ItemChanged_EventName,
                async (obj) =>
                {
                    await this.ExecuteLoadItemsCommand();
                }
            );
        }
        protected override async Task AfterLoadItems(ActsQueryParam queryParam)
        {
            await base.AfterLoadItems(queryParam);

            if (queryParam.ProductId.HasValue)
            {
                var product = _productsService.GetEntityView(queryParam.ProductId.Value);
                this.Product = product;
            }

            this.IsProductLableVisible = this.Product != null;
        }

        void ResetFilter()
        {
            var filterProps = ActListFilterProps.GetDefault(_settings);
            
            if (this.ProductId != null)
            {
                filterProps.ProductId = this.ProductId;
            }
            
            this.Setup(filterProps);
        }
    }
}