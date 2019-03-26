using AView = Android.Views;
using Android.Runtime;
using Android.Views;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Content;
using Android.Renderscripts;
using Android.Util;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.RadialMenu;
using Xamarin.Forms.RadialMenu.AndroidCore;
using Xamarin.Forms.RadialMenu.Models;

[assembly: ExportRenderer(typeof(RadialMenuItem), typeof(RadialMenuItemRenderer))]
namespace Xamarin.Forms.RadialMenu.AndroidCore
{

    public class RadialMenuItemRenderer : VisualElementRenderer<Xamarin.Forms.View>
    {
        public RadialMenuItemRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            //if (e.NewElement != null)
            //{
            //    var dragView = Element as RadialMenuItem;
            //    if (dragView.Title == "OuterCircle"&&dragView.IsShadowVisible)
            //    {
                   
            //        this.SetBackgroundResource(Resource.Drawable.rounded_border);
            //    }
            //    this.Elevation = 2f;
            //}

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var dragView = Element as RadialMenuItem;
            if (dragView.Title == "OuterCircle" && dragView.IsShadowVisible)
            {

                this.SetBackgroundResource(Resource.Drawable.rounded_border);
            }
            this.Elevation = 2f;
        }



    }

}
