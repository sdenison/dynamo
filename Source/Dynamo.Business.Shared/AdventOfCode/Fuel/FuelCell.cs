namespace Dynamo.Business.Shared.AdventOfCode.Fuel
{
    public class FuelCell
    {
        public int Power { get; }

        public FuelCell(int x, int y, int gridSerialNumber)
        {
            var rackId = x + 10;
            var power = rackId * y;
            power += gridSerialNumber;
            power = power * rackId;
            power = GetHundredsPlace(power);
            power -= 5;
            Power = power;
        }

        public int GetHundredsPlace(int input)
        {
            return (input / 100) % 10;
        }
    }
}
