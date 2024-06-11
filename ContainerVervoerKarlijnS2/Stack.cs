using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoerKarlijnS2
{
    public class Stack
    {
        private List<Container> containers = new List<Container>();
        public IReadOnlyList<Container> Containers => containers.AsReadOnly();
        int TotalStackWeight => containers.Sum(c => c.Weight);
        public bool HasValuableContainer => containers.Any(c => c.IsValuable);

        public bool CanAddContainer(Container container)
        {
            if (TotalStackWeight + container.Weight > 120000)
            {
                return false;
            }
            if (container.IsValuable && HasValuableContainer)
            {
                return false;
            }
            return true;
        }

        public bool AddContainer(Container container)
        {
            if (!CanAddContainer(container))
            {
                return false;
            }
            containers.Add(container);
            return true;
        }
    }
}
