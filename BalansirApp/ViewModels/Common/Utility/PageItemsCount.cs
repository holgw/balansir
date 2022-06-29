namespace BalansirApp.ViewModels.Common.Utility
{
    public class PageSize : CycledIntValue
    {
        // CTOR
        public PageSize(int value) : base(0, 1000, value)
        {
        }
    }
}
