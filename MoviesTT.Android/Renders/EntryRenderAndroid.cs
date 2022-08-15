using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using MoviesTT.Droid.Renders;
using MoviesTT.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryRender), typeof(EntryRenderAndroid))]
namespace MoviesTT.Droid.Renders
{
    public class EntryRenderAndroid : EntryRenderer 
    {
        public EntryRenderAndroid(Context context) : base(context) { }

        public EntryRender ElementV2 => Element as EntryRender;
        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackgroundControl(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EntryRender.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryRender.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == EntryRender.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackgroundControl(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected override void UpdateBackground()
        {
            UpdateBackgroundControl(Control);
        }
    }
}

