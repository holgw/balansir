using System;
using System.Collections.Generic;

namespace BalansirApp.Core.Migrations.Tools.Interfaces
{
    public interface IDataDefinitionBase
    {
        List<Exception> Exceptions { get; }

        IDataDefinitionTable<TTable> AddTable<TTable>();
    }
}