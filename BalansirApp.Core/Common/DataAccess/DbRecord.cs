using SQLite;

namespace BalansirApp.Core.Common.DataAccess
{
    public abstract class DbRecord : IIdentifier
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // METHODS: Public
        public bool IsSameRecord(DbRecord record)
        {
            return record.Id == Id;
        }
    }
}
