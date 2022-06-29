using Android.Content;
using BalansirApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundBorderDatePicker), typeof(BalansirApp.Droid.RoundBorderDatePickerRendererDroid))]
namespace BalansirApp.Droid
{
    public class RoundBorderDatePickerRendererDroid : DatePickerRenderer
    {
        RoundBorderHelper _helper;

        public RoundBorderDatePickerRendererDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null && Element is ICanBeValidated)
                _helper = new RoundBorderHelper(Control, Element as ICanBeValidated);
            //Control.Text = (Element as RoundBorderDatePicker).PlaceHolder;
            //(Element as ICanBeValidated).ValidateChange = (obj) => 
            //{
            //    if (!obj)
            //        Control.Text = (Element as RoundBorderDatePicker).PlaceHolder;
            //};
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _helper.UpdateBorderByPropertyName(e.PropertyName);
        }
    }
}