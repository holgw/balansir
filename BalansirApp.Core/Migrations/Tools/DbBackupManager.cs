using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Migrations.Tools.Interfaces;
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
            string originFilePath = _appFilesLocator.DbPath;
            string backupFilePath = this.GetBackupFilePath();

            if (File.Exists(originFilePath))
            {
                int c = 0;
                do
                {
                    c++;
                } while (File.Exists(this.GetBackupFilePath(c)));

                File.Copy(originFilePath, this.GetBackupFilePath(c));
            }
        }

        string GetBackupFilePath(int? c = null)
        {
            string originPath = _appFilesLocator.DbPath;
            string timeStamp = DateTime.Now.ToString("dd_MM_yyyy_HH-mm-ss");
            string filePath = $"{originPath}_{timeStamp}";

            if (c != null)
                filePath += $"_{c}";

            return filePath;
        }
    }
}
