# Xamarin.Forms-RadialMenu
Xamarin.Forms simple radial menu without renderers


based on :
https://github.com/alanbeech/roundfiltermenu/tree/master/Idx.RoundFilterMenu



# Structured documents
  - Create a reusable RadialMenu without relying of any CustomRenderer
 


# Usage
**1.- Just reference it in your View and set MenuItemsSource property:**
```

   <Grid>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition Height="40"></RowDefinition>
        </Grid.RowDefinitions>
        <local:RadialMenu MenuItemsSource="{Binding MenuItems}" 
                          MainMenuCloseButtonImageSource="close_circle"  
                          OuterCircleImageSource="outer_circle" 
                          MainMenuImageSource="menu_circle"  
                          x:Name="Menu" HorizontalOptions="Center" 
                          VerticalOptions="Center" ></local:FilterMenu>
        <Label x:Name="Notifier" Grid.Row="1" HorizontalTextAlignment="Center"></Label>
    </Grid>
  ```  
    
    
**2.- And your ViewModel code will look similar to:**
```
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
```

## Enumerations
Project contain an enumeration which is used to set each menu item location within RadialMenu. 
By default it follows clockwise to animate items when showing(First North then North-East and so on).
```sh
        public enum RadialMenuLocation
        {
            N = 0,//North
            Ne = 1,//North-East
            Nw = 7,//North-West
            S = 4,//South
            Se = 3,//South-East
            Sw = 5,//South-West
            W = 6,//West
            E = 2//East
        }
        
```
 ![Image of Yaktocat](https://image.shutterstock.com/image-vector/wind-rose-cardinal-points-star-260nw-1011439111.jpg)


# Missing Features
- Add Nested lists to each RadialMenuItem
- Add different animations to show radial menu and RadialMenuItems




**Happy coding! :sparkles: :camel: :boom:**
