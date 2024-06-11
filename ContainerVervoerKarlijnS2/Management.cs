using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoerKarlijnS2
{
    public class Management
    {
        public Ship ship { get; private set; }

        public Management(int width, int length)
        {
            ship = new Ship(width, length);
        }

        public void DistributeContainers(List<Container> containers, List<Container> leftList, List<Container> rightList)
        {
            if (containers.Sum(c => c.Weight) > ship.MaxShipWeight)
            {
                throw new ArgumentOutOfRangeException("containers", "The total weight of the containers exceeds the maximum weight of the ship.");
            }

            var sortedContainers = containers.OrderByDescending(c => c.Weight).ToList();
            var cooledValuableContainers = sortedContainers.Where(c => c.IsCooled && c.IsValuable).ToList();
            var cooledContainers = sortedContainers.Where(c => c.IsCooled && !c.IsValuable).ToList();
            var valuableContainers = sortedContainers.Where(c => c.IsValuable && !c.IsCooled).ToList();
            var normalContainers = sortedContainers.Where(c => !c.IsCooled && !c.IsValuable).ToList();

            DistributeListContainers(cooledContainers, leftList, rightList);
            DistributeListContainers(normalContainers, leftList, rightList);
            DistributeListContainers(cooledValuableContainers, leftList, rightList);
            DistributeListContainers(valuableContainers, leftList, rightList);
        }

        private void DistributeListContainers(List<Container> containers, List<Container> leftList, List<Container> rightList)
        {
            int leftWeight = leftList.Sum(c => c.Weight);
            int rightWeight = rightList.Sum(c => c.Weight);

            foreach (var container in containers)
            {
                if (leftWeight <= rightWeight)
                {
                    leftList.Add(container);
                    leftWeight += container.Weight;
                }
                else
                {
                    rightList.Add(container);
                    rightWeight += container.Weight;
                }
            }
        }

        public void PlaceContainersOnShip(List<Container> leftList, List<Container> rightList)
        {
            bool hasCooledContainers = leftList.Any(c => c.IsCooled) || rightList.Any(c => c.IsCooled);

            PlaceContainersOfTypeOnShip(leftList.Where(c => c.IsCooled && !c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: true, placeOnLeftSide: true);
            PlaceContainersOfTypeOnShip(rightList.Where(c => c.IsCooled && !c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: true, placeOnLeftSide: false);

            PlaceContainersOfTypeOnShip(leftList.Where(c => !c.IsCooled && !c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: !hasCooledContainers, placeOnLeftSide: true);
            PlaceContainersOfTypeOnShip(rightList.Where(c => !c.IsCooled && !c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: !hasCooledContainers, placeOnLeftSide: false);

            PlaceValuableContainersOnShip(leftList.Where(c => c.IsCooled && c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: true, placeOnLeftSide: true);
            PlaceValuableContainersOnShip(rightList.Where(c => c.IsCooled && c.IsValuable).ToList(), placeEveryTwoRows: false, placeInFirstRow: true, placeOnLeftSide: false);

            PlaceValuableContainersOnShip(leftList.Where(c => c.IsValuable && !c.IsCooled).ToList(), placeEveryTwoRows: true, placeInFirstRow: false, placeOnLeftSide: true);
            PlaceValuableContainersOnShip(rightList.Where(c => c.IsValuable && !c.IsCooled).ToList(), placeEveryTwoRows: true, placeInFirstRow: false, placeOnLeftSide: false);
        }

        private void PlaceContainersOfTypeOnShip(List<Container> containers, bool placeEveryTwoRows, bool placeInFirstRow, bool placeOnLeftSide)
        {
            int width = ship.Width;
            int halfWidth = (width + 1) / 2;
            int startRow = placeInFirstRow ? 0 : 1;
            int rowStep = placeEveryTwoRows ? 2 : 1;

            foreach (var container in containers)
            {
                bool placed = false;
                for (int row = startRow; row < ship.Length; row += rowStep)
                {
                    for (int col = (placeOnLeftSide ? 0 : halfWidth); col < (placeOnLeftSide ? halfWidth : width); col++)
                    {
                        if (ship.AddContainerToStack(row, col, container))
                        {
                            placed = true;
                            break;
                        }
                    }
                    if (placed) break;
                }
            }
        }

        private void PlaceValuableContainersOnShip(List<Container> containers, bool placeEveryTwoRows, bool placeInFirstRow, bool placeOnLeftSide)
        {
            PlaceContainersOfTypeOnShip(containers, placeEveryTwoRows, placeInFirstRow, placeOnLeftSide);
        }
    }
}
