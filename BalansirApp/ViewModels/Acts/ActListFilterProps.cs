using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using System;

namespace BalansirApp.ViewModels.Acts
{
    /// <summary>
    /// Исходные парметры фильтра, используемые при открытии формы
    /// </summary>
    public struct ActListFilterProps
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? ProductId { get; set; }

        public static ActListFilterProps GetDefault(ISettingsProvider settings)
        {
            DateTime end = DateTime.Now.GetDayDate().AddDays(1);
            DateTime start = end.AddDays(-settings.HistoryDaysCount);

            return new ActListFilterProps()
            {
                Start = start,
                End = end,
            };
        }
    }
}