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

[assembly: ExportRenderer(typeof(RadialMenu), typeof(DraggableMenuRenderer))]
namespace Xamarin.Forms.RadialMenu.AndroidCore
{

    public class DraggableMenuRenderer : VisualElementRenderer<Xamarin.Forms.View>
    {
        float originalX;
        float originalY;
        float dX;
        float dY;
        bool firstTime = true;
        bool touchedDown = false;
        bool hasmoved = false;
        private DisplayMetrics displayMetrics;
        int sH , sW;
        int axisAdditionX = 140;
        int axisAdditionY = 140;
        public DraggableMenuRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            

            if (e.OldElement != null)
            {
                LongClick -= HandleLongClick;
            }
            if (e.NewElement != null)
            {
                displayMetrics = Context.Resources.DisplayMetrics;
                sH = displayMetrics.HeightPixels;
                sW = displayMetrics.WidthPixels;
                //For larger DPI, double value for containment logic so it can adjust accordingly to screen real size
                if (displayMetrics.Density >= 4.0)
                {
                    //"xxxhdpi";
                    axisAdditionX = 280;
                    axisAdditionY = 280;
                }
                else if (displayMetrics.Density >= 3.0 && displayMetrics.Density < 4.0)
                {
                    //xxhdpi
                    axisAdditionX = 280;
                    axisAdditionY = 280;
                }
                else if (displayMetrics.Density >= 2.0)
                {
                    //xhdpi
                }
                else if (displayMetrics.Density >= 1.5 && displayMetrics.Density < 2.0)
                {
                    //hdpi
                }
                else if (displayMetrics.Density >= 1.0 && displayMetrics.Density < 1.5)
                {
                    //mdpi
                }

                LongClick += HandleLongClick;
                var dragView = Element as RadialMenu;
                Click += DraggableMenuRenderer_Click;

                dragView.RestorePositionCommand = new Command(() =>
                {
                    if (!firstTime)
                    {
                        SetX(originalX);
                        SetY(originalY);
                    }

                });
            }

        }

        private void DraggableMenuRenderer_Click(object sender, System.EventArgs e)
        {
            var dragView = Element as RadialMenu;
            if (!dragView.IsOpened)
            {
                dragView.IsOpened = true;

                float currentCenterX = this.GetX();
                float currentCenterY = this.GetY();

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
                    if ((currentCenterY + axisAdditionY) >= sH-160)
                    {
                        currentCenterY = ((sH-(axisAdditionY*2)-80));
                    }

                    SetX(currentCenterX);
                    SetY(currentCenterY);
                }
                //Left X is good but Y top is not
                if (currentCenterY <= axisAdditionY)
                {
                    currentCenterY += (axisAdditionY - currentCenterY);
                    if (currentCenterX <= axisAdditionX)
                    {
                        currentCenterX += (axisAdditionX - currentCenterX);
                    }
                    SetX(currentCenterX);
                    SetY(currentCenterY);
                }
                //Left X is good but Y bottom is not
                if ((currentCenterY + axisAdditionY) >= (sH-160))
                {
                    currentCenterY = ((sH - (axisAdditionY * 2) - 80));
                    SetX(currentCenterX);
                    SetY(currentCenterY);
                }

                //Is on the right side?
                if ((currentCenterX + axisAdditionX) >= (sW-160))
                {
                    currentCenterX = (sW - (axisAdditionX*2));

                    //Upper Y axis
                    if (currentCenterY <= axisAdditionY)
                    {
                        currentCenterY += (axisAdditionY - currentCenterY);
                    }
                    //Bottom Y Axis
                    if ((currentCenterY + axisAdditionY) >= sH - 160)
                    {
                        currentCenterY = ((sH - (axisAdditionY * 2) - 80));
                    }
                    SetX(currentCenterX);
                    SetY(currentCenterY);
                }
                if(!dragView.IsChildItemOpened)
                    dragView.OpenMenu();

          
            }
        }

        private void HandleLongClick(object sender, LongClickEventArgs e)
        {
            var dragView = Element as RadialMenu;
            if (firstTime)
            {
                originalX = GetX();
                originalY = GetY();
                firstTime = false;
            }
            dragView.DragStarted();
            touchedDown = true;
        }

        protected override void OnVisibilityChanged(AView.View changedView, [GeneratedEnum] ViewStates visibility)
        {
            base.OnVisibilityChanged(changedView, visibility);
            if (visibility == ViewStates.Visible)
            {

            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
           
            float x = e.RawX;
            float y = e.RawY;
            var dragView = Element as RadialMenu;

            //if (!dragView.IsOpened)
            //{
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if (dragView.DragMode == RadialMenu.DragMod.Touch)
                    {
                        if (!touchedDown)
                        {
                            if (firstTime)
                            {
                                originalX = GetX();
                                originalY = GetY();
                                firstTime = false;
                            }

                            dragView.DragStarted();
                        }

                        touchedDown = true;
                    }

                    dX = x - this.GetX();
                    dY = y - this.GetY();

                    break;
                case MotionEventActions.Move:
                    float newX = x - dX;
                    var newY = y - dY;
                    if (touchedDown)
                    {
                        if (dragView.DragDirection == RadialMenu.DragDirectionType.All ||
                            dragView.DragDirection == RadialMenu.DragDirectionType.Horizontal)
                        {
                            if ((newX <= 0 || newX >= sW - Width))
                                break;

                            SetX(newX);
                        }

                        if (dragView.DragDirection == RadialMenu.DragDirectionType.All ||
                            dragView.DragDirection == RadialMenu.DragDirectionType.Vertical)
                        {
                            if ( (newY <= 0 || newY >= sH - Height))
                                break;
                            SetY(newY);
                        }
                        hasmoved = true;
                    }

                    break;
                case MotionEventActions.Up:
                    touchedDown = false;
                    dragView.DragEnded();
                    if (hasmoved)
                    {
                        dragView.IsOpened = false;
                        hasmoved = false;
                    }

                    break;
                case MotionEventActions.Cancel:
                    touchedDown = false;
                    break;
                    //}
            }

            return base.OnTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            var dragView = Element as RadialMenu;

            BringToFront();
            if (dragView.IsOpened)
                return false;
            return true;

        }
    }

}
