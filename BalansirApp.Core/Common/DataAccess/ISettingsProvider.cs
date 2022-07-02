namespace BalansirApp.Core.Common.DataAccess
{
    public interface ISettingsProvider
    {
        int HistoryDaysCount { get; set; }
        int PageSize { get; set; }
    }
}
