using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace BalansirApp.Components
{
    internal class BarcodeScanHelper
    {
        private readonly INavigation _navigation;

        // CTOR
        public BarcodeScanHelper(INavigation navigation)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
        }

        public async Task ScanBarcode(Action<string> scanResultHandler)
        {
            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = false,
                TryHarder = true
            };

            var overlay = new ZXingDefaultOverlay
            {
                TopText = "Сканирование штрих-кода",
                BottomText = "Направьте камеру на штрих-код продукта"
            };

            var QRScanner = new ZXingScannerPage(options, overlay);

            QRScanner.OnScanResult += (result) =>
            {
                // Stop scanning
                QRScanner.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                    _navigation.PopModalAsync(true);
                    scanResultHandler(result.Text);
                });
            };

            await _navigation.PushModalAsync(QRScanner);
        }
    }
}
