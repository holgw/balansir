using Xamarin.Forms;

namespace BalansirApp.Controls
{
    public class MinMaxValidatorBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create("MinValue", typeof(int), typeof(MinMaxValidatorBehavior), 0);

        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create("MaxValue", typeof(int), typeof(MinMaxValidatorBehavior), 0);

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
        }

        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newTextValue = e.NewTextValue;

            if (newTextValue == string.Empty || newTextValue == null)
                newTextValue = "0";

            bool ch = int.TryParse(newTextValue, out int newVal);
            if (ch)
            {
                if (newVal < this.MinValue || newVal > this.MaxValue)
                {
                    ((Entry)sender).Text = e.OldTextValue;
                }
            }
            else
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;

        }
    }
}
