using Telerik.Windows.Controls;

namespace HelpFunctionality
{
    public partial class MyHelpWindow : RadWindow
    {
        private static MyHelpWindow instance;

        private MyHelpWindow() 
        { 
            InitializeComponent(); 
        }

        public static MyHelpWindow Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new MyHelpWindow();
                }
                return instance;
            }
        }
    }
}
