using Maui.RadialMenu.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.RadialMenu
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadialMenu : ContentView
    {
        public event EventHandler<Enumerations.Enumerations.RadialMenuLocation> ItemTapped;
        public event EventHandler<ChildItemTapped> ChildItemTapped;
        public event EventHandler MenuClosed;
        public event EventHandler MenuOpened;
        private static RadialMenuItem ParentInBackgroundItem { get; set; }
        
        /// <summary>
        /// Gets or sets the command that will fire when menu item is tapped
        /// </summary>
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(RadialMenu), null);
        public ICommand ItemTappedCommand
        {
            get => (ICommand)GetValue(ItemTappedCommandProperty);
            set => SetValue(ItemTappedCommandProperty, value);
        }
        
        
        /// <summary>
        /// Gets or sets the command that will fire when menu child item is tapped
        /// </summary>
        public static readonly BindableProperty ChildItemTappedCommandProperty =
            BindableProperty.Create(nameof(ChildItemTappedCommand), typeof(ICommand), typeof(RadialMenu), null);
        public ICommand ChildItemTappedCommand
        {
            get => (ICommand)GetValue(ChildItemTappedCommandProperty);
            set => SetValue(ChildItemTappedCommandProperty, value);
        }
        

        
        /// <summary>
        /// Each menu item hiding animation duration in milliseconds .Default value is 80
        /// </summary>
        public static readonly BindableProperty MenuItemHidingDurationProperty =
BindableProperty.Create(nameof(MenuItemHidingDuration), typeof(int), typeof(RadialMenu), 80);
        public int MenuItemHidingDuration
        {
            get
            {
                return (int)GetValue(MenuItemHidingDurationProperty);
            }
            set
            {
                SetValue(MenuItemHidingDurationProperty, value);
            }
        }


        /// <summary>
        /// Each menu item appearing animation duration in milliseconds .Default value is 100
        /// </summary>
        public static readonly BindableProperty MenuItemAppearingDurationProperty =
BindableProperty.Create(nameof(MenuItemAppearingDuration), typeof(int), typeof(RadialMenu), 100);
        public int MenuItemAppearingDuration
        {
            get
            {
                return (int)GetValue(MenuItemAppearingDurationProperty);
            }
            set
            {
                SetValue(MenuItemAppearingDurationProperty, value);
            }
        }


        /// <summary>
        /// Child 'grow' animation duration in milliseconds .Default value is 600
        /// </summary>
        public static readonly BindableProperty ChildGrowAnimationDurationProperty =
BindableProperty.Create(nameof(ChildGrowAnimationDuration), typeof(int), typeof(RadialMenu), 600);
        public int ChildGrowAnimationDuration
        {
            get
            {
                return (int)GetValue(ChildGrowAnimationDurationProperty);
            }
             set
            {
                SetValue(ChildGrowAnimationDurationProperty, value);
            }
        }

        /// <summary>
        /// Child 'shrink' animation duration in milliseconds .Default value is 600
        /// </summary>
        public static readonly BindableProperty ChildShrinkAnimationDurationProperty =
BindableProperty.Create(nameof(ChildShrinkAnimationDuration), typeof(int), typeof(RadialMenu), 600);
        public int ChildShrinkAnimationDuration
        {
            get
            {
                return (int)GetValue(ChildShrinkAnimationDurationProperty);
            }
             set
            {
                SetValue(ChildShrinkAnimationDurationProperty, value);
            }
        }

        /// <summary>
        /// Open animation duration in milliseconds.Default value is 1000
        /// </summary>
        public static readonly BindableProperty MenuOpenAnimationDurationProperty =
BindableProperty.Create(nameof(MenuOpenAnimationDuration), typeof(int), typeof(RadialMenu), 1000);
        public int MenuOpenAnimationDuration
        {
            get
            {
                return (int)GetValue(MenuOpenAnimationDurationProperty);
            }
             set
            {
                SetValue(MenuOpenAnimationDurationProperty, value);
            }
        }

        /// <summary>
        /// Close animation duration in milliseconds .Default value is 1000
        /// </summary>
        public static readonly BindableProperty MenuCloseAnimationDurationProperty =
BindableProperty.Create(nameof(MenuCloseAnimationDuration), typeof(int), typeof(RadialMenu), 1000);
        public int MenuCloseAnimationDuration
        {
            get
            {
                return (int)GetValue(MenuCloseAnimationDurationProperty);
            }
             set
            {
                SetValue(MenuCloseAnimationDurationProperty, value);
            }
        }

        /// <summary>
        /// Parent to Child 'shrink' menu easing animation
        /// </summary>
        public static readonly BindableProperty ChildShrinkEasingProperty =
 BindableProperty.Create(nameof(ChildShrinkEasing), typeof(Easing), typeof(RadialMenu), Easing.CubicOut);
        public Easing ChildShrinkEasing
        {
            get
            {
                return (Easing)GetValue(ChildShrinkEasingProperty);
            }
             set
            {
                SetValue(ChildShrinkEasingProperty, value);
            }
        }
        /// <summary>
        /// Parent to Child 'grow' menu eaing animation
        /// </summary>
        public static readonly BindableProperty ChildGrowEasingProperty =
BindableProperty.Create(nameof(ChildGrowEasing), typeof(Easing), typeof(RadialMenu), Easing.CubicInOut);
        public Easing ChildGrowEasing
        {
            get
            {
                return (Easing)GetValue(ChildGrowEasingProperty);
            }
             set
            {
                SetValue(ChildGrowEasingProperty, value);
            }
        }
        /// <summary>
        /// Menu 'open' easing animation
        /// </summary>
        public static readonly BindableProperty MenuOpenEasingProperty =
BindableProperty.Create(nameof(MenuOpenEasing), typeof(Easing), typeof(RadialMenu),Easing.BounceIn);
        public Easing MenuOpenEasing
        {
            get
            {
                return (Easing)GetValue(MenuOpenEasingProperty);
            }
             set
            {
                SetValue(MenuOpenEasingProperty, value);
            }
        }
        /// <summary>
        /// Menu 'close' easing animation
        /// </summary>
        public static readonly BindableProperty MenuCloseEasingProperty =
BindableProperty.Create(nameof(MenuCloseEasing), typeof(Easing), typeof(RadialMenu), Easing.BounceOut);
        public Easing MenuCloseEasing
        {
            get
            {
                return (Easing)GetValue(MenuCloseEasingProperty);
            }
             set
            {
                SetValue(MenuCloseEasingProperty, value);
            }
        }

        public static readonly BindableProperty CloseMenuWhenChildTappedProperty =
    BindableProperty.Create(nameof(CloseMenuWhenChildTapped), typeof(bool), typeof(RadialMenu), false);
        public bool CloseMenuWhenChildTapped
        {
            get
            {
                return (bool)GetValue(CloseMenuWhenChildTappedProperty);
            }
             set
            {
                SetValue(CloseMenuWhenChildTappedProperty, value);
            }
        }

        public static readonly BindableProperty IsMenuSandboxEnabledProperty = BindableProperty.Create(nameof(IsMenuSandboxEnabled), typeof(bool), typeof(RadialMenu), true);
        public bool IsMenuSandboxEnabled
        {
            get
            {
                return (bool)GetValue(IsMenuSandboxEnabledProperty);
            }
            set
            {
                SetValue(IsMenuSandboxEnabledProperty, value);
            }
        }

        public static readonly BindableProperty IsChildItemOpenedProperty =
            BindableProperty.Create(nameof(IsChildItemOpened), typeof(bool), typeof(RadialMenu), false);
        public bool IsChildItemOpened
        {
            get
            {
                return (bool)GetValue(IsChildItemOpenedProperty);
            }
            private set
            {
                SetValue(IsChildItemOpenedProperty, value);
            }
        }

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

        public static readonly BindableProperty BackButtonImageSourceProperty =
            BindableProperty.Create(nameof(BackButtonImageSource), typeof(ImageSource), typeof(RadialMenu), default(ImageSource));
        public ImageSource BackButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(BackButtonImageSourceProperty);
            }
            private set
            {
                SetValue(BackButtonImageSourceProperty, value);
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
                    //var source = (Xamarin.Forms.FileImageSource)item.Source;
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

        private RadialMenuItem OrganizeItem(RadialMenuItem _item)
        {
            _item.IsVisible = false;
            if (_item.IsOrganized) return _item;
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
            _item.Draw();
            _item.IsOrganized = true;
            return _item;
        }
        public RadialMenu()
        {
            InitializeComponent();
            InnerButtonClose.IsVisible = false;
            InnerButtonMenu.IsVisible = true;
            BackButton.IsVisible = false;
            HandleMenuCenterClicked();
            HandleCloseClicked();

        }
        private void HandleChildOptionClicked(RadialMenuItem parentItem, Enumerations.Enumerations.RadialMenuLocation value)
        {
            parentItem.GestureRecognizers.Clear();
            parentItem.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    //IsOpened = false;
                    ChildItemTapped?.Invoke(this, new Models.ChildItemTapped() { Parent = ParentInBackgroundItem, ItemTapped = value });
                    ChildItemTappedCommand?.Execute( new Models.ChildItemTapped() { Parent = ParentInBackgroundItem, ItemTapped = value });
                    if (CloseMenuWhenChildTapped)
                    {
                        IsOpened = false;
                        IsChildItemOpened = false;
                        if (!_isAnimating)
                        {
                            _isAnimating = true;

                            InnerButtonMenu.IsVisible = true;
                            InnerButtonClose.IsVisible = false;
                            BackButton.IsVisible = true;

                            await HideButtons(ParentInBackgroundItem.ChildItems);

                            InnerButtonClose.RotateTo(0, _animationDelay);
                            InnerButtonClose.FadeTo(0, _animationDelay);
                           
                            BackButton.RotateTo(0, _animationDelay);
                            BackButton.FadeTo(0, _animationDelay);

                            InnerButtonMenu.RotateTo(0, _animationDelay);
                            InnerButtonMenu.FadeTo(1, _animationDelay);

                            ClearGridButtons();

                            await OuterCircle.ScaleTo(1, (uint)MenuCloseAnimationDuration, MenuCloseEasing);


                            InnerButtonClose.IsVisible = false;
                            BackButton.IsVisible = false;

                            BackButton.GestureRecognizers.Clear();
                            InnerButtonMenu.GestureRecognizers.Clear();
                            InnerButtonClose.GestureRecognizers.Clear();
                            HandleMenuCenterClicked();
                            HandleCloseClicked();

                            foreach (var i in MenuItemsSource)
                            {
                                var item = i as RadialMenuItem;
                                //OrganizeItem(item);
                                 mainGrid.Children.Add(item);
                                //HandleOptionClicked(item, item.Location);

                            }

                            _isAnimating = false;
                        }
                    }
                    else
                    {
                        GoBack();
                    }
       
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void HandleOptionClicked(RadialMenuItem item, Enumerations.Enumerations.RadialMenuLocation value)
        {
            item.GestureRecognizers.Clear();
            item.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    ParentInBackgroundItem = item;
                    ItemTapped?.Invoke(this, value);
                    ItemTappedCommand?.Execute(value);
                    
                    if (item.ChildItems?.Count > 0)
                    {
                        IsChildItemOpened = true;
                        await HideButtons(ParentInBackgroundItem.ChildItems);
                        CloseMenu(item);//Navigate to submenu
                    }
                    else
                    {
                        IsOpened = false;
                        IsChildItemOpened = false;
                        CloseMenu();
                    }
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
        private void HandleBackClicked()
        {
            BackButton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await GoBack();
                }),
                NumberOfTapsRequired = 1
            });

        }
        private async Task GoBack()
        {
            if (!_isAnimating)
            {
                _isAnimating = true;
                IsChildItemOpened = false;
                await HideButtons(ParentInBackgroundItem.ChildItems);

                InnerButtonMenu.IsVisible = false;
                InnerButtonClose.IsVisible = true;
                BackButton.IsVisible = true;
                //await HideButtons(MenuItemsSource);


                InnerButtonMenu.RotateTo(360, _animationDelay);
                InnerButtonMenu.FadeTo(0, _animationDelay);
                BackButton.RotateTo(0, _animationDelay);
                BackButton.FadeTo(0, _animationDelay);
                InnerButtonClose.RotateTo(360, _animationDelay);
                InnerButtonClose.FadeTo(1, _animationDelay);

                await OuterCircle.ScaleTo(1, (uint)ChildShrinkAnimationDuration, ChildShrinkEasing);
                await OuterCircle.ScaleTo(3.3, (uint)ChildGrowAnimationDuration, ChildGrowEasing);
                ClearGridButtons();

                //Show Main buttons again
                for (int j = 0; j < MenuItemsSource.Count; j++)
                {
                    var childItem = MenuItemsSource[j] as RadialMenuItem;
                    OrganizeItem(childItem);
                    mainGrid.Children.Add(childItem);
                    HandleOptionClicked(childItem, childItem.Location);
                }

                await ShowButtons(MenuItemsSource);
                InnerButtonMenu.IsVisible = false;
                BackButton.IsVisible = false;
                _isAnimating = false;

                HandleMenuCenterClicked();
                HandleCloseClicked();
                BackButton.GestureRecognizers.Clear();
            }
        }
        public async Task CloseMenu(RadialMenuItem itemTapped = null)
        {
            if (!_isAnimating)
            {
                if (itemTapped == null)
                {
                    _isAnimating = true;

                    InnerButtonMenu.IsVisible = true;
                    InnerButtonClose.IsVisible = true;
                    await HideButtons(MenuItemsSource);

                    BackButton.RotateTo(0, _animationDelay);
                    BackButton.FadeTo(0, _animationDelay);
                    InnerButtonClose.RotateTo(0, _animationDelay);
                    InnerButtonClose.FadeTo(0, _animationDelay);
                    InnerButtonMenu.RotateTo(0, _animationDelay);
                    InnerButtonMenu.FadeTo(1, _animationDelay);
                    await OuterCircle.ScaleTo(1, (uint)MenuCloseAnimationDuration, MenuCloseEasing);
                    InnerButtonClose.IsVisible = false;
                    BackButton.IsVisible = false;
                    _isAnimating = false;

                    MenuClosed?.Invoke(this, null);
                }
                else
                {
                    _isAnimating = true;

                    InnerButtonMenu.IsVisible = false;
                    InnerButtonClose.IsVisible = true;
                    BackButton.IsVisible = true;
                    await HideButtons(MenuItemsSource);

                    InnerButtonClose.RotateTo(0, _animationDelay);
                    InnerButtonClose.FadeTo(0, _animationDelay);
                    InnerButtonMenu.RotateTo(360, _animationDelay);
                    InnerButtonMenu.FadeTo(1, _animationDelay);
                    BackButton.RotateTo(360, _animationDelay);
                    BackButton.FadeTo(1, _animationDelay);


                    await OuterCircle.ScaleTo(1, (uint)ChildShrinkAnimationDuration, ChildShrinkEasing);
                    await OuterCircle.ScaleTo(3.3, (uint)ChildGrowAnimationDuration, ChildGrowEasing);



                    ClearGridButtons();
                    if (itemTapped.ChildItems != null && itemTapped.ChildItems.Count > 0)
                    {
                        for (int j = 0; j < itemTapped.ChildItems.Count; j++)
                        {
                            var childItem = itemTapped.ChildItems[j];
                            OrganizeItem(childItem);
                            mainGrid.Children.Add(childItem);
                            HandleChildOptionClicked(childItem, childItem.Location);
                        }
                    }
                    await ShowButtons(itemTapped.ChildItems);
                    //ParentInBackgroundItem = itemTapped;

                    InnerButtonClose.IsVisible = false;
                    InnerButtonMenu.IsVisible = false;

                    HandleBackClicked();
                    InnerButtonMenu.GestureRecognizers.Clear();
                    InnerButtonClose.GestureRecognizers.Clear();

                    _isAnimating = false;
                }
            }
        }

        private void HandleMenuCenterClicked()
        {
            InnerButtonMenu.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
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

                await OuterCircle.ScaleTo(3.3, (uint)MenuOpenAnimationDuration, MenuOpenEasing);
                await ShowButtons(MenuItemsSource);
                InnerButtonMenu.IsVisible = false;

                _isAnimating = false;

                MenuOpened?.Invoke(this, null);
            }
        }
        private async Task HideButtons(IList items, uint speed = 25)
        {
            if (items == null || items.Count <= 0) return;
            var list = items as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.AppearingOrder).Reverse();
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                   
                     //i.FadeTo(0, speed);
                    await i.ScaleTo(0, (uint)MenuItemHidingDuration, Easing.SinOut);
                    i.IsVisible = false;
                }
            }
        }

        private async Task ShowButtons(IList items, uint speed = 25)
        {
            if (items == null || items.Count <= 0) return;
            var list = items as IList<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.AppearingOrder);
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {
                    i.IsVisible = true;
                    //i.FadeTo(1, speed);
                    await i.ScaleTo(1, (uint)MenuItemAppearingDuration);
                }
            }
        }
        private void ClearGridButtons()
        {
            if (mainGrid.Children == null || mainGrid.Children.Count <= 0) return;
            var speed = 25U;
            var tmpList = mainGrid.Children.ToList();
            var list = new List<RadialMenuItem>();
            tmpList?.ForEach(i => 
            {
                list.Add(i as RadialMenuItem);
            });
            //var list = mainGrid.Children as List<RadialMenuItem>;
            var orderedList = list?.OrderBy(x => x.AppearingOrder);
            if (orderedList != null)
            {
                foreach (var i in orderedList)
                {

                    if (i.Title != "OuterCircle" && i.Title != "InnerButtonMenu" &&
                        i.Title != "InnerButtonClose" && i.Title != "BackButton")
                        mainGrid.Children.Remove(i);
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
                HideButtons(MenuItemsSource, 0);
                OuterCircle.Draw();
                InnerButtonMenu.Draw();
                InnerButtonClose.Draw();
                BackButton.Draw();
                foreach (RadialMenuItem newItem in MenuItemsSource)
                {
                    OrganizeItem(newItem);
                    if (!mainGrid.Children.Contains(newItem))
                        mainGrid.Children.Add(newItem);
                    var source = (FileImageSource)newItem.Source;
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
                                var source = (FileImageSource)newItem.Source;
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