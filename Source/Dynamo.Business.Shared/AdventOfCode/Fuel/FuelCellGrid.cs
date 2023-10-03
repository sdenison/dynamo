using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Fuel
{
    public class FuelCellGrid
    {
        public FuelCell[,] FuelCells { get; }

        public FuelCellGrid(int gridSize, int gridSerialNumber)
        {
            FuelCell[,] fuelCells = new FuelCell[gridSize, gridSize];
            for (var x = 0; x < gridSize; x++)
                for (var y = 0; y < gridSize; y++)
                    fuelCells[x, y] = new FuelCell(x, y, gridSerialNumber);
            FuelCells = fuelCells;
        }

        public int GetPowerForWindow(int windowX, int windowY)
        {
            //Convert x and y coordinates to zero based box
            var minX = windowX - 1;
            var maxX = windowX + 1;
            var minY = windowY - 1;
            var maxY = windowY + 1;
            var totalPower = 0;
            for (var x = minX; x <= maxX; x++)
                for (var y = minY; y <= maxY; y++)
                    totalPower += FuelCells[x, y].Power;
            return totalPower;
        }

        public Coordinates GetMaxPowerCoordinates()
        {
            var maxPowerX = 0;
            var maxPowerY = 0;
            var maxPower = 0;
            for (int windowX = 1; windowX < 299; windowX++)
                for (int windowY = 1; windowY < 299; windowY++)
                    if (GetPowerForWindow(windowX, windowY) > maxPower)
                    {
                        maxPower = GetPowerForWindow(windowX, windowY);
                        maxPowerX = windowX;
                        maxPowerY = windowY;
                    }

            return new Coordinates(maxPowerX - 1, maxPowerY - 1);
        }
    }

    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
