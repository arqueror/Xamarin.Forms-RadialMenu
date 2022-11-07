using Maui.RadialMenu.Models;
using Maui.RadialMenu.TestApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.RadialMenu.TestApp
{
    partial class MainPage : ContentPage
    {
        //public ObservableCollection<RadialMenuItem> MenuItems;
        public MainMenuViewModel vm;
        public MainPage()
        {

            InitializeComponent();

            //Always set BindingContext before filling MenuItemsSource.
            vm = new MainMenuViewModel();
            BindingContext = vm;

            vm.MenuItems = new ObservableCollection<RadialMenuItem>()
            {
                new CustomizedItem()
                {
                    AppearingOrder = 0,
                    Source = "menu_paint.png",
                    WidthRequest = 38,
                    HeightRequest = 38,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Location = Enumerations.Enumerations.RadialMenuLocation.N,Title="North",
                    ChildItems=new ObservableCollection<RadialMenuItem>()
                    {

                            new RadialMenuItem()
                            {
                                Source = "menu_lorry.png",
                                WidthRequest = 38,
                                HeightRequest = 38,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center,
                                Location = Enumerations.Enumerations.RadialMenuLocation.N

                            },
                        new RadialMenuItem()
                        {
                            Source = "menu_lorry.png",
                            WidthRequest = 38,
                            HeightRequest = 38,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            Location = Enumerations.Enumerations.RadialMenuLocation.Ne
                        }, new RadialMenuItem()
                        {
                            Source = "menu_factory.png",
                            WidthRequest = 38,
                            HeightRequest = 38,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            Location = Enumerations.Enumerations.RadialMenuLocation.E

                        },

                    }
                },
            new RadialMenuItem()
            {
                AppearingOrder = 0,
                Source = "menu_lorry.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Ne,
                ChildItems = new ObservableCollection<RadialMenuItem>()
                    {

                            new RadialMenuItem()
                            {
                                Source = "menu_factory.png",
                                WidthRequest = 38,
                                HeightRequest = 38,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.Center,
                                Location = Enumerations.Enumerations.RadialMenuLocation.N

                            }

                    }
            },
            new RadialMenuItem()
            {
                AppearingOrder = 0,
                Source = "menu_factory.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.E
            },
            new CustomizedItem()
            {
                AppearingOrder = 0,
                Source = "menu_cow.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Se,
                Title = "SE"
            },
            new RadialMenuItem()
            {
                AppearingOrder = 0,
                Source = "menu_plane.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.S
            },
            new RadialMenuItem()
            {
                AppearingOrder = 0,
                Source = "menu_award.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.Enumerations.RadialMenuLocation.Sw
            }};

         

            Menu.ItemTapped += async (sender, location) =>
            {
                Notifier.Text = location.ToString();
                await Task.Delay(2000);
                Notifier.Text = "";

            };
            Menu.ChildItemTapped += async (sender, child) =>
            {
                Notifier.Text = $"Parent:{child.Parent.Location.ToString()} Child:{child.ItemTapped.ToString()}";
                await Task.Delay(5000);
                Notifier.Text = "";

            };

            Menu.MenuOpened += (s, a) => 
            {
                var open = true;
            
            };

            Menu.MenuClosed += (s, a) =>
            {
                var closed = true;

            };
        }

    }

    internal class CustomizedItem : RadialMenuItem
    {
        public override void Draw()
        { 
            var itemGrid = new StackLayout() { Spacing = 0 };
            itemGrid.Children.Add(new Image() { Source = Source });
            var label = new Label() { FontSize = 10, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, TextColor = Colors.White, HorizontalTextAlignment = TextAlignment.Center, Text = Title, Margin = new Thickness(0, 1, 0, 0) };
            itemGrid.Children.Add(label);

            //Tell base class this is the content to draw when Draw() is called internally.
            Content = itemGrid;
            
        }
    }
}
