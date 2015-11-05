using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AsyncData
{
    public class BasicChartVM : DispatchedViewModelBase
    {
        private ObservableCollection<PlotInfo> plotData;
        private List<PlotInfo> pendingItems;
        private int maxItemsCount = 1500;

        private Timer timer;
        private object itemsLock = new object();
        private int randomizerCount;

        public BasicChartVM()
        {
            this.pendingItems = new List<PlotInfo>();
            this.plotData = new ObservableCollection<PlotInfo>();

            this.PopulateData();

            this.timer = new Timer(this.Timer_Elapsed, null, 0, 50);
        }

        public ObservableCollection<PlotInfo> PlotData
        {
            get
            {
                return this.plotData;
            }
        }

        private void PopulateData()
        {
            DateTime now = DateTime.Now;
            for (int i = 0; i < this.maxItemsCount; i++)
            {
                this.AddItemAsync(new PlotInfo { Date = now.AddMilliseconds((i - this.maxItemsCount) * 50), YVal = GetNextY() });
            }
        }

        private void SyncItems()
        {
            List<PlotInfo> pendingItemsCopy;
            lock (this.itemsLock)
            {
                pendingItemsCopy = new List<PlotInfo>(this.pendingItems);
                this.pendingItems.Clear();
            }

            foreach (PlotInfo item in pendingItemsCopy)
            {
                this.plotData.Add(item);
            }

            int count = this.plotData.Count - this.maxItemsCount;
            for (int i = 0; i < count; i++)
            {
                this.plotData.RemoveAt(0);
            }
        }

        private void Timer_Elapsed(object state)
        {
            DateTime now = DateTime.Now;
            this.AddItemAsync(new PlotInfo { Date = now, YVal = this.GetNextY() });
        }

        private void AddItemAsync(PlotInfo plotInfo)
        {
            lock (this.itemsLock)
            {
                this.pendingItems.Add(plotInfo);
            }

            this.ScheduleUpdateItemsInView();
        }

        private void ScheduleUpdateItemsInView()
        {
            this.DispatchAction("SyncItems", this.SyncItems);
        }

        private int GetNextY()
        {
            this.randomizerCount++;
            int i = this.randomizerCount;
            int y = i % 100 + i % 77 + i % 44 + i % 12 + i % 10;

            return y;
        }
    }
}
