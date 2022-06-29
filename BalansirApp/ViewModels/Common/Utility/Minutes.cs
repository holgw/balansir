namespace BalansirApp.ViewModels.Common.Utility
{
    public class Minutes : CycledIntValue
    {
        // CTOR
        public Minutes(int value) : base(0, 59, value)
        {
        }
    }
}
