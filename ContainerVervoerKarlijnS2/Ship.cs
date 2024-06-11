using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoerKarlijnS2
{
    public class Ship
    {
        public int Width { get; private set; }
        public int Length { get; private set; }
        public List<Row> rows { get; private set; }
        public int MaxShipWeight => Length * Width * 150000;

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            rows = new List<Row>();
            for (int i = 0; i < length; i++)
            {
                rows.Add(new Row(width));
            }
        }

        public bool AddContainerToStack(int rowIndex, int stackIndex, Container container)
        {
            if (rowIndex >= 0 && rowIndex < rows.Count)
            {
                Row row = rows[rowIndex];
                Stack stack = row.GetStack(stackIndex);
                return stack.AddContainer(container);
            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "RowIndex is buiten bereik.");
            }
        }
    }
}
