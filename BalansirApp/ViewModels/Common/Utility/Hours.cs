namespace BalansirApp.ViewModels.Common.Utility
{
    public class Hours : CycledIntValue
    {
        // CTOR
        public Hours(int value) : base(0, 23, value)
        {
        }
    }
}
