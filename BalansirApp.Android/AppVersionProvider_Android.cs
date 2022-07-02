using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionProvider_Android))]
namespace BalansirApp.Droid
{
    public class AppVersionProvider_Android : IAppVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var context = Android.App.Application.Context;
                var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);

                return $"{info.VersionName}";
            }
        }
    }
}