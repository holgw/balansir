namespace BalansirApp.Core.Common.DataAccess.Interfaces
{
    public interface IDAO<T, P>
        where T : DbRecord, new()
        where P : BaseQueryParam
    {
        // METHODS: Public
        T[] GetAll(P queryParam);
        ItemsPageQueryResult<T, P> GetPage(P queryParam);
        T TryGet(int id);
        int Delete(int id);
        int Save(T item, bool insertStrict = false);
    }
}
