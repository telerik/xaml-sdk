using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace AddCloseButton
{
    public static class TileViewCommandsExtension
    {
        static TileViewCommandsExtension()
        {
            TileViewCommandsExtension.Delete = new RoutedUICommand("Deletes a tile view item", "Delete", typeof(TileViewCommandsExtension));
        }

        public static RoutedUICommand Delete { get; private set; }
    }
}
