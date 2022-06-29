using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Common.DataAccess.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace Tests.DbTests
{
    public abstract class DaoTests<TRecord, TView, TParam>
        where TParam : BaseQueryParam
    {
        string _dbPath = Path.Combine(Environment.CurrentDirectory, "dbMainTest.db");
        protected IAppFilesLocator _appFilesLocator;
        protected IDbMaintainService _dbMaintainService;

        [TestInitialize]
        public virtual void Startup()
        {
            File.Delete(_dbPath);
            
            var appFilesLocatorMock = new Mock<IAppFilesLocator>();
            appFilesLocatorMock.Setup(x => x.GetDatabasePath()).Returns(_dbPath);
            _appFilesLocator = appFilesLocatorMock.Object;

            // Создадим новый файл БД со структурой данных
            _dbMaintainService = new DbMaintainService(_appFilesLocator);
            _dbMaintainService.InitializeDatabase();
        }
    }   
}
