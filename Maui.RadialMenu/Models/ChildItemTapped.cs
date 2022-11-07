using System;
using System.Collections.Generic;
using System.Text;

namespace Maui.RadialMenu.Models
{
    public class ChildItemTapped
    {
        public RadialMenuItem Parent { get; set; }
        public Enumerations.Enumerations.RadialMenuLocation ItemTapped { get; set; }
    }
}
