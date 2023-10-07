namespace Dynamo.Business.Shared.AdventOfCode.Fuel
{
    public class FuelCellGrid
    {
        public FuelCell[,] FuelCells { get; }
        public int GridSize { get; }
        //Roughly tracking the number of steps to get the max power
        public long Calculations { get; set; }

        public FuelCellGrid(int gridSize, int gridSerialNumber)
        {
            FuelCell[,] fuelCells = new FuelCell[gridSize, gridSize];
            for (var x = 0; x < gridSize; x++)
                for (var y = 0; y < gridSize; y++)
                    fuelCells[x, y] = new FuelCell(x, y, gridSerialNumber);
            GridSize = gridSize;
            FuelCells = fuelCells;
        }

        public int GetPowerForWindow(int leftX, int topY, int windowSize)
        {
            var totalPower = 0;
            for (var x = leftX; x < leftX + windowSize; x++)
                for (var y = topY; y < topY + windowSize; y++)
                {
                    totalPower += FuelCells[x, y].Power;
                    Calculations++;
                }
            return totalPower;
        }

        public int GetPowerForWindow(int windowSize, int currentPower)
        {
            for (var x = 0; x < windowSize; x++)
                currentPower += FuelCells[x, windowSize - 1].Power;
            //Needs to be windowsSize - 1 here so we don't include the corner for both X and Y axis
            for (var y = 0; y < windowSize - 1; y++)
                currentPower += FuelCells[windowSize - 1, y].Power;
            Calculations += (windowSize * 2) - 1;
            return currentPower;
        }

        //Calculating power by subtracting the previous highest row and adding the lowest next row.
        //Using only the deltas makes things much faster.
        public int GetNewPowerWithYShiftedBy1(int windowX, int windowY, int windowSize, int currentPower)
        {
            for (var x = windowX; x < (windowX + windowSize); x++)
            {
                currentPower -= FuelCells[x, windowY - 1].Power;
                currentPower += FuelCells[x, windowY + (windowSize - 1)].Power;
                Calculations += 2;
            }
            return currentPower;
        }

        //Calculating power by subtracting the previous leftmost row and adding the rightmost next row.
        //Using only the deltas makes things much faster.
        public int GetNewPowerWithXShiftedBy1(int windowX, int windowY, int windowSize, int currentPower)
        {
            for (var y = windowY; y < (windowY + windowSize); y++)
            {
                currentPower -= FuelCells[windowX - 1, y].Power;
                currentPower += FuelCells[windowX + (windowSize - 1), y].Power;
                Calculations += 2;
            }
            return currentPower;
        }

        public MaxPowerIdentifier GetMaxPowerCoordinates(int windowSize)
        {
            MaxPowerIdentifier maxPower = new MaxPowerIdentifier(0, 0, 0, 0);
            //Tracks power along the x axis
            //Test the initial power at 0,0
            var xPower = GetPowerForWindow(0, 0, windowSize);
            if (xPower > maxPower.Power)
                maxPower = new MaxPowerIdentifier(0, 0, windowSize, xPower);

            //Outer for loop walks along the x-axis
            for (int windowX = 1; windowX < GridSize - windowSize; windowX++)
            {
                //Test the power where y = 0
                xPower = GetNewPowerWithXShiftedBy1(windowX, 0, windowSize, xPower);
                if (xPower > maxPower.Power)
                    maxPower = new MaxPowerIdentifier(windowX, 0, windowSize, xPower);

                var powerForWindow = xPower;
                //Inner for look walks along the y-axis
                for (int windowY = 1; windowY < GridSize - windowSize; windowY++)
                {
                    //Test the power a x,y
                    powerForWindow = GetNewPowerWithYShiftedBy1(windowX, windowY, windowSize, powerForWindow);
                    if (powerForWindow > maxPower.Power)
                        maxPower = new MaxPowerIdentifier(windowX, windowY, windowSize, powerForWindow);
                }
            }
            return maxPower;
        }

        public MaxPowerIdentifier GetMaxPowerCoordinates(int windowSize, int currentPower)
        {
            MaxPowerIdentifier maxPower = new MaxPowerIdentifier(0, 0, windowSize, currentPower);
            //Outer for loop walks along the x-axis
            for (int windowX = 1; windowX < GridSize - windowSize; windowX++)
            {
                //Test the power where y = 0
                currentPower = GetNewPowerWithXShiftedBy1(windowX, 0, windowSize, currentPower);
                if (currentPower > maxPower.Power)
                    maxPower = new MaxPowerIdentifier(windowX, 0, windowSize, currentPower);

                var powerForWindow = currentPower;
                //Inner for look walks along the y-axis
                for (int windowY = 1; windowY < GridSize - windowSize; windowY++)
                {
                    //Test the power a x,y
                    powerForWindow = GetNewPowerWithYShiftedBy1(windowX, windowY, windowSize, powerForWindow);
                    if (powerForWindow > maxPower.Power)
                        maxPower = new MaxPowerIdentifier(windowX, windowY, windowSize, powerForWindow);
                }
            }
            return maxPower;
        }

        public MaxPowerIdentifier GetMaxPower()
        {
            var maxPower = 0;
            MaxPowerIdentifier maxPowerIdentifier = new MaxPowerIdentifier(0, 0, 0, 0);
            var currentPower = 0;
            for (var windowSize = 1; windowSize <= GridSize; windowSize++)
            {
                if (windowSize == 1)
                    currentPower = FuelCells[0, 0].Power;
                else
                    currentPower = GetPowerForWindow(windowSize, currentPower);
                var maxPowerForWindow = GetMaxPowerCoordinates(windowSize, currentPower);
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
