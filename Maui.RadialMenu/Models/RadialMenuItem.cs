using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace  Maui.RadialMenu.Models
{
    public class RadialMenuItem : ContentView
    {
        public bool IsOrganized { get; internal set; } = false;
        //Close,Open or Layout item?
        public bool IsDefaultButton { get; set; } = false;
        public Enumerations.Enumerations.RadialMenuLocation Location { get; set; }
        public int AppearingOrder { get; set; }
        public ObservableCollection<RadialMenuItem> DetailItems { get; set; }
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(ImageSource), typeof(RadialMenuItem), default(ImageSource));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(RadialMenuItem), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty IsShadowVisibleProperty =
         BindableProperty.Create(nameof(IsShadowVisible), typeof(bool), typeof(RadialMenuItem), false);
        public bool IsShadowVisible
        {
            get
            {
                return (bool)GetValue(IsShadowVisibleProperty);
            }
             set
            {
                SetValue(IsShadowVisibleProperty, value);
            }
        }

        public static readonly BindableProperty ChildItemsProperty =
            BindableProperty.Create(nameof(ChildItems), typeof(ObservableCollection<RadialMenuItem>), typeof(RadialMenuItem), null);
        public ObservableCollection<RadialMenuItem> ChildItems
        {
            get
            {
                return (ObservableCollection<RadialMenuItem>)GetValue(ChildItemsProperty);
            }
            set
            {
                if(value!=null)
                    SetValue(ChildItemsProperty, value);
            }
        }

        public virtual void Draw()
        {
            var itemGrid = new Grid();
            if (Source != null)
            {
                itemGrid.Children.Add(new Image() { Source=this.Source});
                Content=itemGrid;
            }
        }

    }
}
