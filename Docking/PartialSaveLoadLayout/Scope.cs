using System;

namespace PartialSaveLoadLayout
{
    public class Scope : IDisposable
    {
        public bool IsActive { get; private set; }

        public Scope()
        {
            this.IsActive = true;
        }

        public void Dispose()
        {
            this.IsActive = false;
        }
    }

    public class RightScope : Scope
    {
    }

    public class LeftScope : Scope
    {
    }
}