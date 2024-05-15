using System.IO;
using System.Reflection;
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

            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("MultiLineComments.query.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    this.syntaxEditor.Document = new TextDocument(reader);
                }
            }

            var daxTagger = new DAXTagger(this.syntaxEditor);
            if (!this.syntaxEditor.TaggersRegistry.IsTaggerRegistered(daxTagger))
            {
                this.syntaxEditor.TaggersRegistry.RegisterTagger(daxTagger);
            }
        }
    }
}
