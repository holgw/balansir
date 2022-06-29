using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using BalansirApp.Core.Common.UseCases.Interfaces;
using System;

namespace BalansirApp.Core.Common.UseCases
{
    public abstract class AbstractGetView_UseCase<TRecord, TView, TParam> : IGetView_UseCase<TView>
        where TRecord : DbRecord, new()
        where TView : class
        where TParam : BaseQueryParam
    {
        private readonly IDAO<TRecord, TParam> _dao;

        // CTOR
        protected AbstractGetView_UseCase(IDAO<TRecord, TParam> dao)
        {
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        // METHODS: Public
        public TView Execute(int id)
        {
            var record = _dao.TryGet(id);

            if (record != null)
                return BuildView(record);
            else
                return null;
        }

        // METHODS: Protected
        protected abstract TView BuildView(TRecord record);
    }
}
