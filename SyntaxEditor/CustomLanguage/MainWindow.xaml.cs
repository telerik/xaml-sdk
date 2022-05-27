using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.SyntaxEditor.Taggers;
using Telerik.Windows.Controls.SyntaxEditor.UI;
using Telerik.Windows.DragDrop;
using Telerik.Windows.SyntaxEditor.Core.Tagging;
using Telerik.Windows.SyntaxEditor.Core.Text;

namespace CustomLanguage
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader reader = new StreamReader("../../script.py"))
            {
                this.syntaxEditor.Document = new TextDocument(reader);
            }

            var pythonTagger = new PythonTagger(this.syntaxEditor);
            if (!this.syntaxEditor.TaggersRegistry.IsTaggerRegistered(pythonTagger))
            {
                this.syntaxEditor.TaggersRegistry.RegisterTagger(pythonTagger);
            }

            this.syntaxEditor.TextFormatDefinitions.AddLast(ClassificationTypes.NumberLiteral, new TextFormatDefinition(new SolidColorBrush(Colors.Red)));
            this.syntaxEditor.TextFormatDefinitions.AddLast(ClassificationTypes.Operator, new TextFormatDefinition(new SolidColorBrush(Colors.YellowGreen)));
            this.syntaxEditor.TextFormatDefinitions.AddLast(PythonTagger.FruitsClassificationType, new TextFormatDefinition(new SolidColorBrush(Colors.LightCoral)));
        }
    }
}
