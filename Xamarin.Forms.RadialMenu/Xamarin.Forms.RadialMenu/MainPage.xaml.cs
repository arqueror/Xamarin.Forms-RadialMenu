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
            vm.MenuItems = new ObservableCollection<RadialMenuItem>()
            {
                new RadialMenuItem()
                {
                    Source = "menu_paint",
                    WidthRequest = 38,
                    HeightRequest = 38,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Location = Enumerations.Enumerations.RadialMenuLocation.N
                }
            };
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_lorry",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Ne
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_factory",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.E
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_cow",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Se
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_plane",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.S
            });
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_award",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Sw
            });


            Menu.ItemTapped += async (sender, e) =>
            {
                var evnt = (SelectedItemChangedEventArgs)e;
                Notifier.Text = (string)evnt.SelectedItem;
                await Task.Delay(2000);
                Notifier.Text = "";

            };
        }

    }
}
