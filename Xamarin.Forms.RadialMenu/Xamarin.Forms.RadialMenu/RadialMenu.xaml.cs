using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public static readonly BindableProperty IsShadowVisibleProperty =
             BindableProperty.Create(nameof(IsShadowVisible), typeof(bool), typeof(RadialMenu), false);
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

        public static readonly BindableProperty OuterCircleImageSourceProperty =
            BindableProperty.Create(nameof(OuterCircleImageSource), typeof(ImageSource), typeof(RadialMenu), default(ImageSource));
        public ImageSource OuterCircleImageSource
        {
            get
            {
                return (ImageSource)GetValue(OuterCircleImageSourceProperty);
            }
            private set
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
        public static readonly BindableProperty IsOpenedProperty =
            BindableProperty.Create(nameof(IsOpened), typeof(bool), typeof(RadialMenu), default(bool));
        public bool IsOpened
        {
            get
            {
                return (bool)GetValue(IsOpenedProperty);
            }
            set
            {
                SetValue(IsOpenedProperty, value);
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
            _item.IsVisible = false;
            _item.Draw();
        }
        public RadialMenu()
        {
            InitializeComponent();
            InnerButtonClose.IsVisible = false;
            InnerButtonMenu.IsVisible = true;
            HandleMenuCenterClicked();
            HandleCloseClicked();

        }

        private void HandleOptionClicked(RadialMenuItem item, Enumerations.Enumerations.RadialMenuLocation value)
        {
            
            item.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    IsOpened = false;
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
                    IsOpened = false;
                    await CloseMenu();

                }),
                NumberOfTapsRequired = 1
            });

        }

        public async Task CloseMenu()
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
                Command = new Command(async () => {
                    IsOpened = true;
                    await OpenMenu(); 
                }),
                NumberOfTapsRequired = 1
            });

        }

        public async Task OpenMenu()
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
        }
        private async Task HideButtons(uint speed=25)
        {
            if (MenuItemsSource == null || MenuItemsSource.Count <= 0) return;
            var list = MenuItemsSource as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.Location).Reverse();
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                    i.IsVisible = false;
                    await i.FadeTo(0, speed);
                }
            }
        }

        private async Task ShowButtons()
        {
            if (MenuItemsSource == null || MenuItemsSource.Count <= 0) return;
            var speed = 25U;
            var list = MenuItemsSource as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.Location);
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                    i.IsVisible = true;
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
                HideButtons(0);
                            OuterCircle.Draw();
                    InnerButtonMenu.Draw();
                    InnerButtonClose.Draw();
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
        #region DraggableMembers
        public event EventHandler DragStart = delegate { };
        public event EventHandler DragEnd = delegate { };

        public static readonly BindableProperty DragDirectionProperty = BindableProperty.Create(
            propertyName: "DragDirection",
            returnType: typeof(DragDirectionType),
            declaringType: typeof(RadialMenu),
            defaultValue: DragDirectionType.All,
            defaultBindingMode: BindingMode.TwoWay);

        public enum DragMod
        {
            Touch,
            LongPress
        }
        public enum DragDirectionType
        {
            All,
            Vertical,
            Horizontal
        }
        public DragDirectionType DragDirection
        {
            get { return (DragDirectionType)GetValue(DragDirectionProperty); }
            set { SetValue(DragDirectionProperty, value); }
        }


        public static readonly BindableProperty DragModeProperty = BindableProperty.Create(
           propertyName: "DragMode",
           returnType: typeof(DragMod),
           declaringType: typeof(RadialMenu),
           defaultValue: DragMod.LongPress,
           defaultBindingMode: BindingMode.TwoWay);

        public DragMod DragMode
        {
            get { return (DragMod)GetValue(DragModeProperty); }
            set { SetValue(DragModeProperty, value); }
        }

        public static readonly BindableProperty IsDraggingProperty = BindableProperty.Create(
          propertyName: "IsDragging",
          returnType: typeof(bool),
          declaringType: typeof(RadialMenu),
          defaultValue: false,
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly BindableProperty RestorePositionCommandProperty = BindableProperty.Create(nameof(RestorePositionCommand), typeof(ICommand), typeof(RadialMenu), default(ICommand), BindingMode.TwoWay, null, OnRestorePositionCommandPropertyChanged);

        static void OnRestorePositionCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as RadialMenu;
            if (source == null)
            {
                return;
            }
            source.OnRestorePositionCommandChanged();
        }

        private void OnRestorePositionCommandChanged()
        {
            OnPropertyChanged("RestorePositionCommand");
        }

        public ICommand RestorePositionCommand
        {
            get
            {
                return (ICommand)GetValue(RestorePositionCommandProperty);
            }
            set
            {
                SetValue(RestorePositionCommandProperty, value);
            }
        }

        public void DragStarted()
        {
            DragStart(this, default(EventArgs));
            IsDragging = true;
        }

        public void DragEnded()
        {
            IsDragging = false;
            DragEnd(this, default(EventArgs));
        }
        #endregion
    }
}