using BalansirApp.Core.Domains.Acts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActCellView : ContentView
    {
        private bool _IsExpanded;
        private bool _IsExpanding;

        private double? _height;

        public static readonly BindableProperty ActViewProperty = BindableProperty.Create(
            propertyName: "ActView",
            returnType: typeof(ActView),
            declaringType: typeof(ActCellView),
            defaultValue: default(string),
            propertyChanged: (b, o, n) =>
            {
                var actCellView = (ActCellView)b;
                var actView = (ActView)n;
                actCellView.DeltaLabel.TextColor =
                    (actCellView.ActView.Delta >= 0) ? Color.FromHex("19A8F5") : Color.FromHex("E45F75");
            });

        public ActView ActView
        {
            get => (ActView)GetValue(ActViewProperty);
            set => SetValue(ActViewProperty, value);
        }

        // CTOR
        public ActCellView()
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