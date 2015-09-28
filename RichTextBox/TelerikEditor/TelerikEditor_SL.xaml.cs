using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace TelerikEditorDemo
{
    /// <summary>
    /// Interaction logic for TelerikEditor.xaml
    /// </summary>
    public partial class TelerikEditor : UserControl
    {
        public TelerikEditor()
        {
            InitializeComponent();
            IconSources.ChangeIconsSet(IconsSet.Modern);
        }
    }
}