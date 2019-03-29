using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.RadialMenu.Models
{
    public class ChildItemTapped
    {
        public RadialMenuItem Parent { get; set; }
        public Enumerations.Enumerations.RadialMenuLocation ItemTapped { get; set; }
    }
}
