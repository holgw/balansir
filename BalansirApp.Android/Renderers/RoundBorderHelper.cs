using Android.Graphics.Drawables;
using Android.Views;
using BalansirApp.Controls;
using Xamarin.Forms.Platform.Android;

namespace BalansirApp.Droid
{
    public class RoundBorderHelper
    {
        View _control;
        ICanBeValidated _element;

        public RoundBorderHelper(View control, ICanBeValidated element)
        {
            _control = control;
            _element = element;
            UpdateBorder();
        }

        public void UpdateBorder()
        {
            var gd = new GradientDrawable();

            gd.SetColor(_element.BorderColor.ToAndroid());
            gd.SetStroke((int)_element.BorderWidth * 2, _element.BorderColor.ToAndroid());
            gd.SetCornerRadius((float)_element.BorderRadius);

            _control.SetPadding(15, 15, 15, 15);
            _control.SetBackground(gd);
        }

        public void UpdateBorderByPropertyName(string propertyName)
        {
            switch (propertyName)
            {
                case "BorderColor":
                case "BorderRadius":
                case "BorderWidth":
                    UpdateBorder();
                    break;
                default:
                    return;
            }
        }
    }
}