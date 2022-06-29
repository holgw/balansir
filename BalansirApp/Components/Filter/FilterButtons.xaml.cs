using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Components.Filter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterButtons : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: "Command",
            returnType: typeof(Command),
            declaringType: typeof(FilterButtons),
            defaultValue: default(Command));

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public FilterButtons()
        {
            InitializeComponent();
        }
    }
}