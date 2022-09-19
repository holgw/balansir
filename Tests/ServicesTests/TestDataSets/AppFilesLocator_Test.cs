using BalansirApp.Core.Common.DataAccess;
using System;

namespace Tests.ServicesTests.TestDataSets
{
    class AppFilesLocator_Test : BaseAppFilesLocator
    {
        public override string DbFolder => Environment.CurrentDirectory;
    }
}
