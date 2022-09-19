using System.IO;
using System;

namespace BalansirApp.Core.Common.DataAccess
{
    public class BaseAppFilesLocator : IAppFilesLocator
    {
        public string DbName => "dbMain";

        public virtual string DbFolder => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public string DbPath => Path.Combine(this.DbFolder, this.DbName);

        public string ConnectionString => $"Data Source={this.DbPath};Version=3;";

        public string LogsFolder => Path.Combine(this.DbFolder, "Logs");
    }
}
