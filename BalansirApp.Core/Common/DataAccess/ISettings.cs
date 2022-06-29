namespace BalansirApp.Core.Common.DataAccess
{
    public interface ISettings
    {
        int HistoryDaysCount { get; set; }
        int PageSize { get; set; }
    }
}
