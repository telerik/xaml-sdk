using System;
using System.Collections.Generic;

namespace ExportUIElement
{
    internal class CompositeDisposableObject : IDisposable
    {
        private List<IDisposable> components = new List<IDisposable>();

        public void Add(IDisposable component)
        {
            if (component != null)
            {
                this.components.Add(component);
            }
        }

        public void Dispose()
        {
            foreach (var component in this.components)
            {
                component.Dispose();
            }

            this.components.Clear();
        }
    }
}
