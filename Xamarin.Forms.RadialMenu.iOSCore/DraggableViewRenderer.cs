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

[assembly: ExportRenderer(typeof(RadialMenu), typeof(DraggableViewRenderer))]
namespace Xamarin.Forms.RadialMenu.iOSCore
{

    public class DraggableViewRenderer : VisualElementRenderer<View>
    {
        bool longPress = false;
        bool firstTime = true;
        double lastTimeStamp = 0f;
        UIPanGestureRecognizer panGesture;
        CGPoint lastLocation;
        CGPoint originalPosition;
        UIGestureRecognizer.Token panGestureToken;
        CGRect displayMetrics;
        nfloat sH, sW;
        bool isFixingfMenuPosition = false;
        void DetectPan()
        {
            var dragView = Element as RadialMenu;
            var ne=Xamarin.Forms.Platform.iOS.Platform.GetRenderer(Element).NativeView;
            if (longPress || dragView.DragMode == RadialMenu.DragMod.Touch)
            {
                if (panGesture.State == UIGestureRecognizerState.Began)
                {
                    dragView.DragStarted();
                    if (firstTime)
                    {
                        originalPosition = Center;
                        firstTime = false;
                    }
                }

                CGPoint translation = panGesture.TranslationInView(Superview);
                var currentCenterX = Center.X;
                var currentCenterY = Center.Y;
                if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                {
                    currentCenterX = lastLocation.X + translation.X;
                }

                if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                {
                    currentCenterY = lastLocation.Y + translation.Y;
                }
                if (((currentCenterX >= 30 && currentCenterX <= sW-30))&&((currentCenterY >= 30 && currentCenterY <= sH - 30)))
                    Center = new CGPoint(currentCenterX, currentCenterY);
              
                if (panGesture.State == UIGestureRecognizerState.Ended)
                {
                    
                    dragView.DragEnded();
                    longPress = false;
                    lastLocation = Center;   
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
          
            if (e.OldElement != null)
            {
                RemoveGestureRecognizer(panGesture);
                panGesture.RemoveTarget(panGestureToken);

            }
            if (e.NewElement != null)
            {
                displayMetrics = UIScreen.MainScreen.Bounds;
                sH = displayMetrics.Height;
                sW = displayMetrics.Width;

                var dragView = Element as RadialMenu;
                panGesture = new UIPanGestureRecognizer();
                panGestureToken = panGesture.AddTarget(DetectPan);
                AddGestureRecognizer(panGesture);
                

                dragView.RestorePositionCommand = new Command(() =>
                {
                    if (!firstTime)
                    {

                        Center = originalPosition;
                    }
                });

            }

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as RadialMenu;
            if (e.PropertyName == "IsOpened")
            {
                if (dragView.IsOpened)
                {
                    CGPoint translation = panGesture.TranslationInView(Superview);
                    var currentCenterX = Center.X;
                    var currentCenterY = Center.Y;
                    int axisAdditionX = 100;
                    int axisAdditionY = 125;
                    //SMART CONTAINMENT LOGIC
                    //Is on the left side?
                    if (currentCenterX <= axisAdditionX)
                    {
                        currentCenterX += (axisAdditionX - currentCenterX);
                        //Upper Y axis
                        if (currentCenterY <= axisAdditionY)
                        {
                            currentCenterY += (axisAdditionY - currentCenterY);
                        }
                        //Bottom Y Axis
                        if ((currentCenterY+axisAdditionY) >= sH)
                        {
                            currentCenterY = (sH- axisAdditionY);
                        }
                       
                        Center = new CGPoint(currentCenterX, currentCenterY);
                    }

                    //Left X is good but Y top is not
                    if (currentCenterY <= axisAdditionY)
                    {
                        currentCenterY += (axisAdditionY - currentCenterY);
                        if (currentCenterX <= axisAdditionX)
                        {
                            currentCenterX += (axisAdditionX - currentCenterX);
                        }
                        Center = new CGPoint(currentCenterX, currentCenterY);
                    }

                    //Left X is good but Y bottom is not
                    if ((currentCenterY + axisAdditionY) >= sH)
                    {
                        currentCenterY = (sH - axisAdditionY);
                        Center = new CGPoint(currentCenterX, currentCenterY);
                    }

                    //Is on the right side?
                    if ((currentCenterX + axisAdditionX) >= sW)
                    {
                        currentCenterX = (sW - axisAdditionX);

                        //Upper Y axis
                        if (currentCenterY <= axisAdditionY)
                        {
                            currentCenterY += (axisAdditionY - currentCenterY);
                        }
                        //Bottom Y Axis
                        if ((currentCenterY + axisAdditionY) >= sH)
                        {
                            currentCenterY = (sH - axisAdditionY);
                        }

                        Center = new CGPoint(currentCenterX, currentCenterY);
                    }
                    lastLocation = Center;
                }
            }
            base.OnElementPropertyChanged(sender, e);

        }
       
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            lastTimeStamp = evt.Timestamp;
            Superview.BringSubviewToFront(this);
           
                lastLocation = Center;
            
        }
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            if ((Element as RadialMenu).IsOpened) return;
            if (evt.Timestamp - lastTimeStamp >= 0.5)
            {
                longPress = true;
            }
            base.TouchesMoved(touches, evt);
        }
    }
}