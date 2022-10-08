using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppFilesLocator_Android))]
namespace BalansirApp.Droid
{
    public class AppFilesLocator_Android : BaseAppFilesLocator
    {
        public override string DbFolder => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    }
}