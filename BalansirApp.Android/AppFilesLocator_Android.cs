using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Droid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IAppFilesLocator))]
namespace BalansirApp.Droid
{
    public class AppFilesLocator_Android : BaseAppFilesLocator
    {
        public override string DbFolder => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    }
}