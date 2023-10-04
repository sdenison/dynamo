using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Dynamo.Business.Shared.AdventOfCode.Fuel
{
    public class FuelCellGrid
    {
        public FuelCell[,] FuelCells { get; }
        public int GridSize => FuelCells.GetLength(0);

        public FuelCellGrid(int gridSize, int gridSerialNumber)
        {
            FuelCell[,] fuelCells = new FuelCell[gridSize, gridSize];
            for (var x = 0; x < gridSize; x++)
                for (var y = 0; y < gridSize; y++)
                    fuelCells[x, y] = new FuelCell(x, y, gridSerialNumber);
            FuelCells = fuelCells;
        }

        public int GetPowerForWindow_original(int windowX, int windowY)
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

        public int GetPowerForWindow(int leftX, int topY, int windowSize)
        {
            //Convert x and y coordinates to zero based box
            var minX = leftX;
            var maxX = (leftX) + windowSize;
            var minY = topY;
            var maxY = (topY) + windowSize;
            var totalPower = 0;
            for (var x = leftX; x < maxX; x++)
                for (var y = topY; y < maxY; y++)
                    totalPower += FuelCells[x, y].Power;
            return totalPower;
        }

        public MaxPowerIdentifier GetMaxPowerCoordinates(int windowSize)
        {
            var maxPowerX = 0;
            var maxPowerY = 0;
            var maxPower = 0;
            for (int windowX = 1; windowX < 300 - windowSize; windowX++)
                for (int windowY = 1; windowY < 300 - windowSize; windowY++)
                    if (GetPowerForWindow(windowX, windowY, windowSize) > maxPower)
                    {
                        maxPower = GetPowerForWindow(windowX, windowY, windowSize);
                        maxPowerX = windowX;
                        maxPowerY = windowY;
                    }
            return new MaxPowerIdentifier(maxPowerX, maxPowerY, windowSize, maxPower);
        }


        public MaxPowerIdentifier GetMaxPower()
        {
            var maxPower = 0;
            MaxPowerIdentifier maxPowerIdentifier = null;
            for (var windowSize = 1; windowSize <= GridSize; windowSize++)
            {
                var maxPowerForWindow = GetMaxPowerCoordinates(windowSize);
                if (maxPowerForWindow.Power > maxPower)
                {
                    maxPowerIdentifier = maxPowerForWindow;
                    maxPower = maxPowerForWindow.Power;
                }
            }
            return maxPowerIdentifier;
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

    public class MaxPowerIdentifier
    {
        public Coordinates Coordinates { get; }
        public int WindowSize { get; }
        public int Power { get; }

        public MaxPowerIdentifier(int x, int y, int windowSize, int power)
        {
            Coordinates = new Coordinates(x, y);
            WindowSize = windowSize;
            Power = power;
        }
    }
}
