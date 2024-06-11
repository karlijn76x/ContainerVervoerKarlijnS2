using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoerKarlijnS2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instellen van de lengte en breedte van het schip
            Console.WriteLine("Voer de breedte van het schip in:");
            int width = int.Parse(Console.ReadLine());

            Console.WriteLine("Voer de lengte van het schip in:");
            int length = int.Parse(Console.ReadLine());

            // Maak een Management instantie aan
            Management management = new Management(width, length);

            // Maak een lijst van containers aan
            List<Container> containers = new List<Container>
            {
               new Container(12000, true, false),
    new Container(8000, false, true),
    new Container(10000, true, true),
    new Container(6000, false, false),
    new Container(15000, false, false),
    new Container(9000, true, false),
    new Container(11000, false, true),
    new Container(13000, true, true),
    new Container(7000, false, false),
    new Container(14000, false, true)
            };

            // Verdeel de containers in linkse en rechtse lijsten
            List<Container> leftList = new List<Container>();
            List<Container> rightList = new List<Container>();

            management.DistributeContainers(containers, leftList, rightList);

            // Plaats de containers op het schip
            management.PlaceContainersOnShip(leftList, rightList);

            // Toon de indeling van het schip
            DisplayShipLayout(management.ship);
        }

        static void DisplayShipLayout(Ship ship)
        {
            Console.WriteLine("\nContainers op het schip:");

            for (int rowIndex = 0; rowIndex < ship.Length; rowIndex++)
            {
                Console.WriteLine($"Rij {rowIndex + 1}:");

                for (int colIndex = 0; colIndex < ship.Width; colIndex++)
                {
                    var stack = ship.rows[rowIndex].GetStack(colIndex);
                    Console.WriteLine($" Kolom {colIndex + 1}:");

                    if (stack.Containers.Any())
                    {
                        foreach (var container in stack.Containers)
                        {
                            Console.WriteLine($"  - {container}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  - Leeg");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
