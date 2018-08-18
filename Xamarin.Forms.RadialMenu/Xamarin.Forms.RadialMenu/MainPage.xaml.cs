using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.RadialMenu.Models;
using Xamarin.Forms.RadialMenu.ViewModels;

namespace Xamarin.Forms.RadialMenu
{
	public partial class MainPage : ContentPage
	{
        //public ObservableCollection<RadialMenuItem> MenuItems;
        public MainMenuViewModel vm;
        public MainPage()
        {
           
            InitializeComponent();
            vm = new MainMenuViewModel();
            BindingContext = vm;
            var custItem=
            vm.MenuItems = new ObservableCollection<RadialMenuItem>()
            {
                new CustomizedItem()
                {
                    Source = "menu_paint.png",
                    WidthRequest = 38,
                    HeightRequest = 38,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Location = Enumerations.Enumerations.RadialMenuLocation.N,Title="North"
                }
            };
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_lorry.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Ne
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_factory.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.E
            });
            vm.MenuItems.Add(new CustomizedItem()
            {
                Source = "menu_cow.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Se,Title="SE"
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_plane.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.S
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_award.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Sw
            });


            Menu.ItemTapped += async (sender, location) =>
            {
                Notifier.Text = location.ToString();
                await Task.Delay(2000);
                Notifier.Text = "";

            };
        }

    }

    public class CustomizedItem:RadialMenuItem
    {
        public override void Draw()
        {
            var itemGrid = new StackLayout(){Spacing=0};
            if (Source != null)
            {
               
                itemGrid.Children.Add(new Image(){Source=Source});
                var label = new Label() {FontSize=11,VerticalOptions = LayoutOptions.End,TextColor=Color.White,HorizontalTextAlignment=TextAlignment.Center,Text= Title, Margin=new Thickness(0,1,0,0)};
                itemGrid.Children.Add(label);
                Content=itemGrid;
            }
        }
    }
}
