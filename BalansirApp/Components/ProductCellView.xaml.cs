using BalansirApp.Core.Products;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCellView : ContentView
    {
        private bool _IsExpanded;
        private bool _IsExpanding;

        private double? _height;

        public static readonly BindableProperty ProductProperty = BindableProperty.Create(
            propertyName: "Product",
            returnType: typeof(ProductView),
            declaringType: typeof(ProductCellView),
            defaultValue: default(string),
            propertyChanged: (b, o, n) =>
            {
                var productCellView = (ProductCellView)b;
                var productView = (ProductView)n;
                productCellView.BalanceLabel.TextColor =
                    (productView?.Balance >= 0) ? Color.FromHex("#6DCFB1") : Color.FromHex("#E45F75");
            });

        public ProductView Product
        {
            get => (ProductView)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }

        // CTOR
        public ProductCellView()
        {
            InitializeComponent();
            ExpandableLayout.HeightRequest = 0;
        }

        private async void Title_Clicked(object sender, EventArgs e)
        {
            if (!_IsExpanding)
            {
                _IsExpanding = true;
                _height = _height ?? ExpandableContent.Height;

                if (_IsExpanded)
                {
                    var animation = new Animation(v => ExpandableLayout.HeightRequest = v, _height.Value, 0);
                    await ExpandableLayout.FadeTo(0, 250);
                    animation.Commit(this, "ExpandSize", 16, 250);

                }
                else
                {
                    var animation = new Animation(v => ExpandableLayout.HeightRequest = v, 0, _height.Value);
                    animation.Commit(this, "ExpandSize", 16, 250);
                    await ExpandableLayout.FadeTo(1, 250);
                }
                _IsExpanded = !_IsExpanded;
                _IsExpanding = false;
            }
        }
    }
}