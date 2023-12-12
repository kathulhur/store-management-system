using NetBarcode;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace StoreManagementSystemX.Services
{
    public class BarcodeImageService : IBarcodeImageService
    {
        private static readonly Barcode _barcodeInstance = (new Barcode()).Configure((BarcodeSettings settings) =>
        {
            settings.BarcodeType = BarcodeType.EAN13;
            settings.ShowLabel = false;
        });

        public BarcodeImageService() { }

        public BitmapSource GenerateBarcodeImage(string barcode)
        {
            return BitmapToBitmapSource(_barcodeInstance.GetImage(barcode));
        }

        private BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr32, null,

                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }
    }
}
