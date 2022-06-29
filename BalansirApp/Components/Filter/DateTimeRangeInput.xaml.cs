using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BalansirApp.Components.Filter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimeRangeInput : ContentView
    {
        public static readonly BindableProperty StartProperty = BindableProperty.Create(
            propertyName: "Start",
            returnType: typeof(DateTime),
            declaringType: typeof(DateTimeRangeInput),
            defaultValue: default(DateTime));

        public static readonly BindableProperty EndProperty = BindableProperty.Create(
            propertyName: "End",
            returnType: typeof(DateTime),
            declaringType: typeof(DateTimeRangeInput),
            defaultValue: default(DateTime));

        public DateTime Start
        {
            get => (DateTime)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }

        public DateTime End
        {
            get => (DateTime)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }

        // CTOR
        public DateTimeRangeInput()
        {
            InitializeComponent();
            StartDatePicker.Date = this.Start;
            EndDatePicker.Date = this.End;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == StartProperty.PropertyName)
            {
                if (this.Start > this.End)
                {
                    this.End = this.Start;
                    StartDatePicker.Date = this.End;
                }
                else
                {
                    StartDatePicker.Date = this.Start;
                }
            }
            else if (propertyName == EndProperty.PropertyName)
            {
                if (this.Start > this.End)
                {
                    this.Start = this.End;
                    EndDatePicker.Date = this.Start;
                }
                else
                {
                    EndDatePicker.Date = this.End;
                }
            }
        }
    }
}