using BalansirApp.Core.Common.DataAccess;

namespace BalansirApp.ViewModels.Common
{
    public class Shell_ViewModel : BaseViewModel
    {
        private readonly IAppVersionProvider _appVersionProvider;

        public string AppName => $"BALANSIR {_appVersionProvider.AppVersion}";

        // CTOR
        public Shell_ViewModel(IAppVersionProvider appVersionProvider)
        {
            _appVersionProvider = appVersionProvider ?? throw new System.ArgumentNullException(nameof(appVersionProvider));
        }
    }
}
