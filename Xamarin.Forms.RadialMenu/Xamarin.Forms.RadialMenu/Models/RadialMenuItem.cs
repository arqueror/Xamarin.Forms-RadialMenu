using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Xamarin.Forms.RadialMenu.Models
{
    public class RadialMenuItem : Image
    {
        public Enumerations.Enumerations.RadialMenuLocation Location { get; set; }
        public ObservableCollection<RadialMenuItem> DetailItems { get; set; }
    }
}
