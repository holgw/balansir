using System;
using Xamarin.Forms;

namespace BalansirApp.Controls
{
    public interface ICanBeValidated
    {
        Action<bool> ValidateChange { get; set; }

        bool IsValidated { get; set; }

        Xamarin.Forms.Color BorderColor { get; set; }

        double BorderWidth { get; set; }

        double BorderRadius { get; set; }
    }

    public class RoundBorderDatePicker : DatePicker, ICanBeValidated
    {
        public RoundBorderDatePicker()
        {
            // It seems that all the picker need to validated
            IsValidated = false;
            BorderColor = Color.Red;
            this.DateSelected += RoundBorderDatePicker_DateSelected;
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
                                                                          "BorderWidth",
                                                                          typeof(double),
                                                                          typeof(RoundBorderDatePicker),
                                                                          2.0);

        public double BorderWidth
        {
            get
            {
                return (double)GetValue(BorderWidthProperty);
            }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
                                                                          "BorderColor",
                                                                          typeof(Color),
                                                                          typeof(RoundBorderDatePicker),
                                                                          Color.FromHex("#807A79"));

        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty BorderRadiusProperty = BindableProperty.Create(
                                                                          "BorderRadius",
                                                                          typeof(double),
                                                                          typeof(RoundBorderDatePicker),
                                                                          20.0);

        public double BorderRadius
        {
            get
            {
                return (double)GetValue(BorderRadiusProperty);
            }
            set
            {
                SetValue(BorderRadiusProperty, value);
            }
        }

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
            "PlaceHolder",
            typeof(string),
            typeof(RoundBorderDatePicker),
            "");

        public string PlaceHolder
        {
            get
            {
                return (string)GetValue(PlaceHolderProperty);
            }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }

        public static readonly BindableProperty MinimumAgeProperty = BindableProperty.Create(
            "MinimumAge",
            typeof(int),
            typeof(RoundBorderDatePicker),
            18);

        public int MinimumAge
        {
            get
            {
                return (int)GetValue(MinimumAgeProperty);
            }
            set
            {
                SetValue(MinimumAgeProperty, value);
            }
        }

        public Action<bool> ValidateChange { get; set; }

        public bool IsValidated { get; set; }

        void RoundBorderDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //if (e.NewDate.Date != DateTime.Now.Date && e.NewDate.Year < (DateTime.Now.Year - MinimumAge))
            //{
            //    IsValidated = true;
            //    if (ValidateChange != null)
            //        ValidateChange(true);
            //    BorderColor = Color.FromHex("#807A79");
            //}
            //else
            //{
            //    IsValidated = false;
            //    if (ValidateChange != null)
            //        ValidateChange(false);
            //    BorderColor = Color.Red;
            //}
        }
    }
}
