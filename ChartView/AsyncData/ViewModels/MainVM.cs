namespace AsyncData
{
    public class MainVM : DispatchedViewModelBase
    {
        private BasicChartVM basicChartVM;
        private AsyncVM asyncVM;

        public MainVM()
        {
            this.basicChartVM = new BasicChartVM();
            this.asyncVM = new AsyncVM();
        }

        public BasicChartVM BasicChartVM
        {
            get
            {
                return this.basicChartVM;
            }
        }

        public AsyncVM AsyncVM
        {
            get
            {
                return this.asyncVM;
            }
        }
    }
}
