using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin.Forms.RadialMenu
{
    public class DragStackLayout : StackLayout
    {
        public bool IsCurrentlyDragging { get; private set; }
        public View FocusedView { get; private set; }

        private readonly IDictionary<View, Thickness> _originalMarginDictionary = new Dictionary<View, Thickness>();
        private View _currentlyHoveredView { get; set; }

        public void NotifyDragStart(View view)
        {
            IsCurrentlyDragging = true;
            FocusedView = view;

            _originalMarginDictionary.Clear();
            foreach (var child in Children)
                _originalMarginDictionary.Add(child, child.Margin);
        }

        public void NotifyDragStop()
        {
            IsCurrentlyDragging = false;

            FocusedView = null;
            _currentlyHoveredView = null;

            RestoreMargins(Children.ToArray());
        }

        public void NotifyHoverPosition(int index)
        {
            try
            {
                if (index == Children.Count || Children.IndexOf(FocusedView) == index ||
                    _currentlyHoveredView == Children[index])
                    return;

                // Reset Margin and Set
                if (_currentlyHoveredView == null || Children[index] != _currentlyHoveredView)
                {
                    if (_currentlyHoveredView != null)
                        RestoreMargins(_currentlyHoveredView);

                    _currentlyHoveredView = Children[index];
                }

                var m = _currentlyHoveredView.Margin;
                var l = Orientation == StackOrientation.Horizontal ? m.Left + FocusedView.Width : m.Left;
                var b = Orientation == StackOrientation.Vertical ? m.Bottom + FocusedView.Height : m.Bottom;

                // Do not display margin on last element and current
                if (index != Children.Count)
                    _currentlyHoveredView.Margin = new Thickness(l, m.Top, m.Right, b);
            }
            finally
            {
                if (Children.IndexOf(FocusedView) == index)
                    _currentlyHoveredView = null;

                RestoreMargins(Children.ToArray());
            }
        }

        private void RestoreMargins(params View[] views)
        {
            foreach (var view in views)
                if (view != _currentlyHoveredView)
                    view.Margin = _originalMarginDictionary[view];
        }
    }
}
