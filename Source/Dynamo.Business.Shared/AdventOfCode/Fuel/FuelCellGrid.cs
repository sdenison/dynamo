namespace Dynamo.Business.Shared.AdventOfCode.Fuel
{
    public class FuelCellGrid
    {
        public FuelCell[,] FuelCells { get; }
        public int GridSize => FuelCells.GetLength(0);
        //Roughly tracking the number of steps to get the max power
        public long Calculations { get; set; }

        public FuelCellGrid(int gridSize, int gridSerialNumber)
        {
            FuelCell[,] fuelCells = new FuelCell[gridSize, gridSize];
            for (var x = 0; x < gridSize; x++)
                for (var y = 0; y < gridSize; y++)
                    fuelCells[x, y] = new FuelCell(x, y, gridSerialNumber);
            FuelCells = fuelCells;
        }

        public int GetPowerForWindow(int leftX, int topY, int windowSize)
        {
            var maxX = leftX + windowSize;
            var maxY = topY + windowSize;
            var totalPower = 0;
            for (var x = leftX; x < maxX; x++)
                for (var y = topY; y < maxY; y++)
                {
                    totalPower += FuelCells[x, y].Power;
                    Calculations++;
                }
            return totalPower;
        }

        //Calculating power by subtracting the previous highest row and adding the lowest next row.
        //Using only the deltas makes things much faster.
        public int GetNewPowerWithYShiftedBy1(int windowX, int windowY, int windowSize, int currentPower)
        {
            if (windowY == 0)
                return GetPowerForWindow(windowX, 0, windowSize);
            for (var x = windowX; x < (windowX + windowSize); x++)
            {
                currentPower = currentPower - FuelCells[x, windowY - 1].Power;
                currentPower = currentPower + FuelCells[x, windowY + (windowSize - 1)].Power;
                Calculations += 2;
            }
            return currentPower;
        }

        public MaxPowerIdentifier GetMaxPowerCoordinates(int windowSize)
        {
            var maxPowerX = 0;
            var maxPowerY = 0;
            var maxPower = 0;
            for (int windowX = 0; windowX < GridSize - windowSize; windowX++)
            {
                var powerForWindow = 0;
                for (int windowY = 0; windowY < GridSize - windowSize; windowY++)
                {
                    powerForWindow = GetNewPowerWithYShiftedBy1(windowX, windowY, windowSize, powerForWindow);
                    if (powerForWindow > maxPower)
                    {
                        maxPower = powerForWindow;
                        maxPowerX = windowX;
                        maxPowerY = windowY;
                    }
                }
            }
            return new MaxPowerIdentifier(maxPowerX, maxPowerY, windowSize, maxPower);
        }

        public MaxPowerIdentifier GetMaxPower()
        {
            var maxPower = 0;
            MaxPowerIdentifier maxPowerIdentifier = null;
            for (var windowSize = 1; windowSize <= GridSize; windowSize++)
            {
                //var maxPowerForWindow = GetMaxPowerCoordinates(windowSize);
                var maxPowerForWindow = GetMaxPowerCoordinates(windowSize);
                if (maxPowerForWindow.Power > maxPower)
                {
                    maxPowerIdentifier = maxPowerForWindow;
                    maxPower = maxPowerForWindow.Power;
                }
            }
            return maxPowerIdentifier;
        }

        #region Unoptimized 
        public MaxPowerIdentifier GetMaxPowerCoordinatesOld(int windowSize)
        {
            var maxPowerX = 0;
            var maxPowerY = 0;
            var maxPower = 0;

            for (int windowX = 0; windowX < GridSize - windowSize; windowX++)
                for (int windowY = 0; windowY < GridSize - windowSize; windowY++)
                {
                    var powerForWindow = GetPowerForWindow(windowX, windowY, windowSize);
                    if (powerForWindow > maxPower)
                    {
                        maxPower = powerForWindow;
                        maxPowerX = windowX;
                        maxPowerY = windowY;
                    }
                }

            return new MaxPowerIdentifier(maxPowerX, maxPowerY, windowSize, maxPower);
        }

        public MaxPowerIdentifier GetMaxPowerOld()
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
        #endregion
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
