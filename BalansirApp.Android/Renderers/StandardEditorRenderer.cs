using Android.Content;
using BalansirApp.Controls;
using BalansirApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StandardEditor), typeof(StandardEditorRenderer))]

namespace BalansirApp.Droid.Renderers
{
    public class StandardEditorRenderer : EditorRenderer
    {
        public StandardEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}