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
        public string GetDatabasePath()
        {
            string dbName = "dbMain";
            string dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(dbFolder, dbName);

            return path;
        }
    }
}