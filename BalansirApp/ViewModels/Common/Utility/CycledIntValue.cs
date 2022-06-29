namespace BalansirApp.ViewModels.Common.Utility
{
    public class CycledValue
    {
        public decimal Min { get; }
        public decimal Max { get; }
        public decimal Value { get; private set; }
        public decimal Step { get; }

        // CTOR
        public CycledValue(decimal min, decimal max, decimal value, decimal step = 1)
        {
            if (min > max)
            {
                decimal temp = min;
                min = max;
                max = temp;
            }

            Min = min;
            Max = max;
            Step = step;

            SetValue(value);
        }

        // METHODS: Public
        public void SetValue(decimal val)
        {
            if (val > Max)
            {
                val = Max;
            }

            if (val < Min)
            {
                val = Min;
            }

            Value = val;
        }
        public void Increment()
        {
            Value += Step;

            if (Value > Max)
                Value = Min;
        }
        public void Decremenet()
        {
            Value -= Step;

            if (Value < Min)
                Value = Max;
        }
    }

    public class CycledIntValue
    {
        public int Min { get; }
        public int Max { get; }
        public int Value { get; private set; }

        // CTOR
        public CycledIntValue(int min, int max, int value)
        {
            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }

            Min = min;
            Max = max;

            SetValue(value);
        }

        // METHODS: Public
        public void SetValue(int val)
        {
            if (val > Max)
            {
                val = Max;
            }

            if (val < Min)
            {
                val = Min;
            }

            Value = val;
        }
        public void Increment()
        {
            Value++;

            if (Value > Max)
                Value = Min;
        }
        public void Decremenet()
        {
            Value--;

            if (Value < Min)
                Value = Max;
        }
    }
}
