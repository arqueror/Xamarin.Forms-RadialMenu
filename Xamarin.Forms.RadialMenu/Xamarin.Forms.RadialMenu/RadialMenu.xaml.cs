using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.RadialMenu.Enumerations;
using Xamarin.Forms;
using Xamarin.Forms.RadialMenu.Models;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.RadialMenu
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RadialMenu : ContentView
	{
        public event EventHandler<Enumerations.Enumerations.RadialMenuLocation> ItemTapped;

        public static readonly BindableProperty OuterCircleImageSourceProperty =
            BindableProperty.Create(nameof(OuterCircleImageSource), typeof(ImageSource), typeof(RadialMenu), default(ImageSource));
        public ImageSource OuterCircleImageSource
        {
            get
            {
                return (ImageSource)GetValue(OuterCircleImageSourceProperty);
            }
            set
            {
                SetValue(OuterCircleImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty MainMenuImageSourceProperty =
            BindableProperty.Create(nameof(MainMenuImageSource), typeof(ImageSource), typeof(RadialMenu), default(ImageSource));
        public ImageSource MainMenuImageSource
        {
            get
            {
                return (ImageSource)GetValue(MainMenuImageSourceProperty);
            }
            set
            {
                SetValue(MainMenuImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty MainMenuCloseButtonImageSourceProperty =
            BindableProperty.Create(nameof(MainMenuCloseButtonImageSource), typeof(ImageSource), typeof(RadialMenu), default(ImageSource));
        public ImageSource MainMenuCloseButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(MainMenuCloseButtonImageSourceProperty);
            }
            set
            {
                SetValue(MainMenuCloseButtonImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty MenuItemsSourceProperty = BindableProperty.Create(nameof(MenuItemsSource), typeof(IList), typeof(RadialMenu), default(IList), propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            ((RadialMenu)bindableObject).ItemsSourceChanged(bindableObject, oldValue, newValue);
        });
        private IList<RadialMenuItem> _items = new ObservableCollection<RadialMenuItem>();
        public IList MenuItemsSource
        {
            get
            {

                return (IList)GetValue(MenuItemsSourceProperty);
            }
            set
            {
                SetValue(MenuItemsSourceProperty, value);
                foreach (var i in value)
                {
                    var item = i as RadialMenuItem;
                    OrganizeItem(item);
                    if (!mainGrid.Children.Contains(item))
                        mainGrid.Children.Add(item);
                    var source = (Xamarin.Forms.FileImageSource)item.Source;
                    HandleOptionClicked(item, item.Location);
                }
            }
        }

        private bool _isAnimating = false;
        private uint _animationDelay = 300;

        private void OrganizeItem(RadialMenuItem _item)
        {
            switch (_item.Location)
            {
                case Enumerations.Enumerations.RadialMenuLocation.N:
                    _item.Margin = new Thickness(0, 0, 0, 130);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.Ne:
                    _item.Margin = new Thickness(90, 0, 0, 90);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.Nw:
                    _item.Margin = new Thickness(0, 0, 90, 90);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.S:
                    _item.Margin = new Thickness(0, 130, 0, 0);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.Se:
                    _item.Margin = new Thickness(90, 90, 0, 0);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.Sw:
                    _item.Margin = new Thickness(0, 90, 90, 0);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.W:
                    _item.Margin = new Thickness(0, 0, 130, 0);
                    break;
                case Enumerations.Enumerations.RadialMenuLocation.E:
                    _item.Margin = new Thickness(130, 0, 0, 0);
                    break;
            }
        }
        public RadialMenu()
        {
            InitializeComponent();
            InnerButtonClose.IsVisible = false;
            InnerButtonMenu.IsVisible = true;
            HandleMenuCenterClicked();
            HandleCloseClicked();

        }

        private void HandleOptionsClicked()
        {
            foreach (var i in MenuItemsSource)
            {
                var item = i as RadialMenuItem;
                var source = (Xamarin.Forms.FileImageSource)item.Source;
                HandleOptionClicked(item, item.Location);
            }

        }

        private void HandleOptionClicked(Image image, Enumerations.Enumerations.RadialMenuLocation value)
        {
            image.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    ItemTapped?.Invoke(this, value);
                    CloseMenu();
                }),
                NumberOfTapsRequired = 1
            });
        }

        private void HandleCloseClicked()
        {
            InnerButtonClose.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await CloseMenu();
                }),
                NumberOfTapsRequired = 1
            });

        }

        private async Task CloseMenu()
        {
            if (!_isAnimating)
            {

                _isAnimating = true;

                InnerButtonMenu.IsVisible = true;
                InnerButtonClose.IsVisible = true;
                await HideButtons();

                InnerButtonClose.RotateTo(0, _animationDelay);
                InnerButtonClose.FadeTo(0, _animationDelay);
                InnerButtonMenu.RotateTo(0, _animationDelay);
                InnerButtonMenu.FadeTo(1, _animationDelay);
                await OuterCircle.ScaleTo(1, 1000, Easing.BounceOut);
                InnerButtonClose.IsVisible = false;

                _isAnimating = false;
            }
        }

        private void HandleMenuCenterClicked()
        {
            InnerButtonMenu.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if (!_isAnimating)
                    {
                        _isAnimating = true;

                        InnerButtonClose.IsVisible = true;
                        InnerButtonMenu.IsVisible = true;

                        InnerButtonMenu.RotateTo(360, _animationDelay);
                        InnerButtonMenu.FadeTo(0, _animationDelay);
                        InnerButtonClose.RotateTo(360, _animationDelay);
                        InnerButtonClose.FadeTo(1, _animationDelay);

                        await OuterCircle.ScaleTo(3.3, 1000, Easing.BounceIn);
                        await ShowButtons();
                        InnerButtonMenu.IsVisible = false;

                        _isAnimating = false;

                    }
                }),
                NumberOfTapsRequired = 1
            });

        }

        private async Task HideButtons()
        {
            var speed = 25U;
            var list = MenuItemsSource as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.Location).Reverse();
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                    await i.FadeTo(0, speed);
                }
            }
        }

        private async Task ShowButtons()
        {
            var speed = 25U;
            var list = MenuItemsSource as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.Location);
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                    await i.FadeTo(1, speed);
                }
            }
        }
        void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (MenuItemsSource == null)
                return;

            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                HideButtons();
                foreach (RadialMenuItem newItem in MenuItemsSource)
                {
                    OrganizeItem(newItem);
                    if (!mainGrid.Children.Contains(newItem))
                        mainGrid.Children.Add(newItem);
                    var source = (Xamarin.Forms.FileImageSource)newItem.Source;
                    HandleOptionClicked(newItem, newItem.Location);
                }

                notifyCollection.CollectionChanged += (sender, e) =>
                {
                    if (e.NewItems != null)
                    {
                        var tempList = new List<object>();
                        if (e.NewItems != null)
                        {
                            foreach (RadialMenuItem newItem in e.NewItems)
                            {
                                tempList.Add(newItem);
                                OrganizeItem(newItem);
                                if (!mainGrid.Children.Contains(newItem))
                                    mainGrid.Children.Add(newItem);
                                var source = (Xamarin.Forms.FileImageSource)newItem.Source;
                                HandleOptionClicked(newItem, newItem.Location);
                            }
                            foreach (var newItem in MenuItemsSource)
                            {
                                var item = newItem as RadialMenuItem;
                                tempList.Add(item);
                            }

                            MenuItemsSource = tempList;
                        }
                    }

                };
            }

        }
    }
}