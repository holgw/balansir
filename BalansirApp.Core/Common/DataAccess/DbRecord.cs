using LinqToDB.Mapping;

namespace BalansirApp.Core.Common.DataAccess
{
    public abstract class DbRecord : IIdentifier
    {
        [PrimaryKey,Column(IsIdentity = true, SkipOnInsert = true)]
        public int Id { get; set; }

        // METHODS: Public
        public bool IsSameRecord(DbRecord record)
        {
            return record.Id == Id;
        }
    }
}
