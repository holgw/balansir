using BalansirApp.Core.Common.DataAccess;
using System;
using System.IO;

namespace BalansirApp.Core.Migrations.Tools
{
    class DbBackupManager : IDbBackupManager
    {
        private readonly IAppFilesLocator _appFilesLocator;

        // CTOR
        public DbBackupManager(IAppFilesLocator appFilesLocator)
        {
            _appFilesLocator = appFilesLocator ?? throw new ArgumentNullException(nameof(appFilesLocator));
        }

        public void BackupFile()
        {
            string originFilePath = _appFilesLocator.GetDatabasePath();
            string backupFilePath = this.GetBackupFilePath();

            File.Copy(originFilePath, backupFilePath);
        }

        string GetBackupFilePath()
        {
            string originPath = _appFilesLocator.GetDatabasePath();
            string timeStamp = DateTime.Now.ToString("dd_MM_yyyy_HH-mm-ss");
            return $"{originPath}_{timeStamp}";
        }
    }
}
