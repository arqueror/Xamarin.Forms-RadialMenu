# Xamarin.Forms-RadialMenu
Xamarin.Forms simple radial menu without renderers. **Android, iOS and UWP Supported**

based on:
https://github.com/alanbeech/roundfiltermenu/tree/master/Idx.RoundFilterMenu



# Motivation
  - Create a reusable RadialMenu without relying on any CustomRenderer
 
 
# Installation
Install NuGet package: https://www.nuget.org/packages/Xamarin.Forms.RadialMenu/
or download repo and compile it manually so you can reference it from your target project :D


# Usage (iOS and Android)
**1.- Just reference it in your View and set MenuItemsSource property:**
```
//Add namespace
         xmlns:radial="clr-namespace:Xamarin.Forms.RadialMenu;assembly=Xamarin.Forms.RadialMenu"

//Create instance
        <radial:RadialMenu MenuItemsSource="{Binding MenuItems}" 
                          MainMenuCloseButtonImageSource="close_circle"  
                          OuterCircleImageSource="outer_circle" 
                          MainMenuImageSource="menu_circle"  
                          x:Name="Menu" HorizontalOptions="Center" 
                          VerticalOptions="Center" ></local:FilterMenu>
  ```  
    
    
**2.- And your ViewModel/Codebehind code will look similar to:**
```
//Add controls to Menu collection
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
 ........
```

# Usage (UWP*)
For UWP you need to put your image files in your application root folder and its mandatory for you to add the extension of your files, otherwise menu won't show up.Probably good idea to use **OnPlatform** for this.
```
//Add namespace
         xmlns:radial="clr-namespace:Xamarin.Forms.RadialMenu;assembly=Xamarin.Forms.RadialMenu"

//Create instance
          <radial:RadialMenu MenuItemsSource="{Binding MenuItems}" 
                          MainMenuCloseButtonImageSource="close_circle.png"   
                          OuterCircleImageSource="outer_circle.png"           
                          MainMenuImageSource="menu_circle.png"              
                          x:Name="Menu" HorizontalOptions="Center" 
                          VerticalOptions="Center" ></radial:RadialMenu>


//Add controls to Menu collection with file extension
vm = new MainMenuViewModel();
            BindingContext = vm;
            var custItem =
            vm.MenuItems = new ObservableCollection<RadialMenuItem>()
            {
                new RadialMenuItem()
                {
                    Source = "menu_paint.png",
                    WidthRequest = 38,
                    HeightRequest = 38,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Location = Enumerations.RadialMenuLocation.N,Title="North"
                }
            };
            vm.MenuItems.Add(new RadialMenuItem()
            {
                Source = "menu_lorry.png",
                WidthRequest = 38,
                HeightRequest = 38,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Location = Enumerations.RadialMenuLocation.Ne
            });
.........

```

## Events
```
  Menu.ItemTapped += async (sender, location) =>
            {
                  var locationTapped = nameof(location);
            };

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

# How it looks
![Image of Yaktocat](https://arqueror.blob.core.windows.net/publicfiles/RadialMenu_01.PNG?raw=false)
![Image of Yaktocat](https://arqueror.blob.core.windows.net/publicfiles/RadialMenu_02.PNG?raw=false)
  
  
  # Adding Custom Content
  Items relies on a base class called **RadialMenuItem**, which exposes a virtual method called **Draw**. By default this method is in charge of drawing each item in the position we manually set in the beginning.
  ```
      public virtual void Draw()
        {
            var itemGrid = new Grid();
            if (Source != null)
            {
                itemGrid.Children.Add(new Image() { Source=this.Source});
                this.Content = itemGrid;
            }
        }
  ```
  In case you want to add other items than Images you can!. Just create a class that inherits from **RadialMenuItem**, override **Draw()** and build your own element (**just make sure to use Labels and Images since tapped event behaves nicely this way**). After this just use it normally when creating the menu items list in your VM.
  ```
  public class CustomizedItem:RadialMenuItem
    {
        public override void Draw()
        {
            var itemGrid = new StackLayout(){Spacing=0};
            if (Source != null)
            {
               
                itemGrid.Children.Add(new Image(){Source=Source});
                var label = new Label() {FontSize=11,
                  VerticalOptions = LayoutOptions.End,TextColor=Color.White,
                  HorizontalTextAlignment=TextAlignment.Center,
                  Text= Title, 
                  Margin=new Thickness(0,1,0,0)};
                itemGrid.Children.Add(label);
                Content = itemGrid;
            }
        }
    }
    
    
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
  ```
  ![Image of Yaktocat](https://arqueror.blob.core.windows.net/publicfiles/RadialMenu_03.PNG?raw=false)
  
  
# Missing Features
- Add Nested lists to each RadialMenuItem
- Add different animations to show radial menu and RadialMenuItems

# Special Thanks
 Thanks to Alan Beech (https://github.com/alanbeech) for the base code used for this repo.
<br/>

**Happy coding! :sparkles: :camel: :boom:**
