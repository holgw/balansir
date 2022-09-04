using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Droid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppFilesLocator_Android))]
namespace BalansirApp.Droid
{
    public class AppFilesLocator_Android : IAppFilesLocator
    {
        public string DbName => "dbMain";

        public string DbFolder => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public string GetDatabasePath()
        {
            string path = Path.Combine(this.DbFolder, this.DbName);

            return path;
        }
    }
}