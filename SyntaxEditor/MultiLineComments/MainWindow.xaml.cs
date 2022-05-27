using System.IO;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls.SyntaxEditor.UI;
using Telerik.Windows.SyntaxEditor.Core.Tagging;
using Telerik.Windows.SyntaxEditor.Core.Text;

namespace MultiLineComments
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader reader = new StreamReader("../../query.txt"))
            {
                this.syntaxEditor.Document = new TextDocument(reader);
            }

            var daxTagger = new DAXTagger(this.syntaxEditor);
            if (!this.syntaxEditor.TaggersRegistry.IsTaggerRegistered(daxTagger))
            {
                this.syntaxEditor.TaggersRegistry.RegisterTagger(daxTagger);
            }
        }
    }
}
