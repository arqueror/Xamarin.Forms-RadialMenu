using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using CoreGraphics;
using Xamarin.Forms.RadialMenu;
using Xamarin.Forms.RadialMenu.iOSCore;
using static Xamarin.Forms.RadialMenu.RadialMenu;
using System;
using Xamarin.Forms.RadialMenu.Models;

[assembly: ExportRenderer(typeof(RadialMenuItem), typeof(RadialMenuItemRenderer))]
namespace Xamarin.Forms.RadialMenu.iOSCore
{

    public class RadialMenuItemRenderer : VisualElementRenderer<View>
    {
       

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            //if (e.NewElement != null)
            //{
            //    var dragView = Element as RadialMenuItem;
            //    if (dragView.Title == "OuterCircle"&&dragView.IsShadowVisible)
            //    {
            //        this.Layer.CornerRadius = 30.0f;
            //        this.Layer.MasksToBounds = false;
            //        this.Layer.BorderWidth = 0.0f;

            //        this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            //        this.Layer.ShadowOpacity = 0.6f;
            //        this.Layer.ShadowRadius = 3;
            //        this.Layer.ShadowOffset = new CGSize(3.0f, 3.0f);
                  
            //    }
            //}


        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var dragView = Element as RadialMenuItem;
            if (dragView.Title == "OuterCircle" && dragView.IsShadowVisible)
            {
                this.Layer.CornerRadius = 30.0f;
                this.Layer.MasksToBounds = false;
                this.Layer.BorderWidth = 0.0f;

                this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
                this.Layer.ShadowOpacity = 0.6f;
                this.Layer.ShadowRadius = 3;
                this.Layer.ShadowOffset = new CGSize(3.0f, 3.0f);

            }
        }

    }
}