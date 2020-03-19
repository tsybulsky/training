namespace DataChecks
{
    public class IntChecker : INumberCheck<int>
    {
        public bool Check(string inputValue)
        {
            return int.TryParse(inputValue, out int _);
        }

        public bool NegativeCheck(string inputValue, out int value)
        {
            if (int.TryParse(inputValue, out value))
            {
                return value < 0;
            }
            else
                return false;
        }

        public bool PositiveCheck(string inputValue, out int value)
        {
            if (int.TryParse(inputValue, out value))
            {
                return value > 0;
            }
            else
                return false;
        }

        public bool RangeCheck(string inputValue, int lowBound, int highBound, out int value)
        {
            if (int.TryParse(inputValue, out value))
            {
                return (value >= lowBound) && (value <= highBound);
            }
            else
                return false;
        }
    }
}
