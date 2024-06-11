using System;
using System.Collections.Generic;

namespace ContainerVervoerKarlijnS2
{
    public class Row
    {
        private List<Stack> stacks = new List<Stack>();
        public IReadOnlyList<Stack> Stacks => stacks.AsReadOnly();

        public Row(int width)
        {
            for (int i = 0; i < width; i++)
            {
                stacks.Add(new Stack());
            }
        }

        public Stack GetStack(int index)
        {
            if (index >= 0 && index < stacks.Count)
            {
                return stacks[index];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and the width of the row.");
            }
        }
    }
}
