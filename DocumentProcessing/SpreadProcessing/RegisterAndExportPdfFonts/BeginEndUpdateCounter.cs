using System;

namespace RegisterAndExportPdfFonts
{
    public class BeginEndUpdateCounter
    {
        private int count = 0;
        private bool shouldExecuteAction;
        private readonly Action onUpdatesFinished;

        public BeginEndUpdateCounter(Action onUpdatesFinished)
        {
            this.shouldExecuteAction = false;
            this.onUpdatesFinished = onUpdatesFinished;
        }

        public void BeginUpdate()
        {
            this.count++;
        }

        public void EndUpdate()
        {
            this.count--;
            this.ValidateCount();
            this.ExecuteAction();
        }

        public void ResumeActionExecution()
        {
            this.shouldExecuteAction = true;
            this.ExecuteAction();
        }

        public void PauseActionExecution()
        {
            this.shouldExecuteAction = false;
        }

        private void ExecuteAction()
        {
            if (this.shouldExecuteAction && this.count == 0)
            {
                this.onUpdatesFinished();
                this.shouldExecuteAction = false;
            }
        }

        private void ValidateCount()
        {
            if (this.count < 0)
            {
                throw new InvalidOperationException("Cannot call EndUpdate method more times compared to BeginUpdate method!");
            }
        }
    }
}
