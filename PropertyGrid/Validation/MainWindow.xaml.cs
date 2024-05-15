using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Validation;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rpg.AutoGeneratingPropertyDefinition += new EventHandler<Telerik.Windows.Controls.Data.PropertyGrid.AutoGeneratingPropertyDefinitionEventArgs>(RpgAutoGeneratingPropertyDefinition);
            rpg.Item = new MyTestClass() { StringProp = "String Value", RequiredField = "Required Field", IntProp = 10, DateTimeProp = new DateTime(1920, 2, 21) };
        }

        void RpgAutoGeneratingPropertyDefinition(object sender, Telerik.Windows.Controls.Data.PropertyGrid.AutoGeneratingPropertyDefinitionEventArgs e)
        {
            //IDataErrorInfo
            (e.PropertyDefinition.Binding as Binding).ValidatesOnDataErrors = true;
            (e.PropertyDefinition.Binding as Binding).NotifyOnValidationError = true;

            // DataAnnotations
            (e.PropertyDefinition.Binding as Binding).ValidatesOnExceptions = true;
        }

        private ObservableCollection<ValidationError> results;

        public ObservableCollection<ValidationError> Results
        {
            get
            {
                if (this.results == null)
                {
                    this.results = new ObservableCollection<ValidationError>();
                }
                return results;
            }
        }
    }
}
