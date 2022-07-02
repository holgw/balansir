using BalansirApp.Core.Common.DataAccess;
using Xamarin.Essentials;

namespace BalansirApp.Utility
{
    /// <summary>
    /// Универсальный класс для хранени янастроек приложения
    /// (базируется на Xamarin.Essentials.Preferences)
    /// </summary>
    public class Settings : ISettingsProvider
    {
        public int HistoryDaysCount
        {
            get => Preferences.Get("HistoryDaysCount", 30);
            set => Preferences.Set("HistoryDaysCount", value);
        }

        public int PageSize
        {
            get => Preferences.Get("PageSize", 30);
            set => Preferences.Set("PageSize", value);
        }
    }
}