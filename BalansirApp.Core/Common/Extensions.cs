using System;

namespace BalansirApp.Core.Common
{
    public static class Extensions
    {
        public static bool IsValidId(this int? id)
        {
            return (id ?? 0) > 0;
        }
        public static void Validate(this int id, string propertyName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException($"Undefined identifier {propertyName}");
            }
        }

        public static DateTime TrySetHour(this DateTime dt, int h)
        {
            try
            {
                return new DateTime(dt.Year, dt.Month, dt.Day, h, dt.Minute, 0);
            }
            catch
            {
                return dt;
            }           
        }

        public static DateTime TrySetMinute(this DateTime dt, int m)
        {
            try
            {
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, m, 0);
            }
            catch
            {
                return dt;
            }            
        }

        public static DateTime SetDayDate(this DateTime dt, DateTime dayDate)
        {
            return new DateTime(dayDate.Year, dayDate.Month, dayDate.Day, dt.Hour, dt.Minute, 0);
        }

        public static DateTime GetDayDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
    }
}
