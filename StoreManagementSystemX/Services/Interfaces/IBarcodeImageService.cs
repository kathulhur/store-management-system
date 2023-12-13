using NetBarcode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StoreManagementSystemX.Services
{
    public interface IBarcodeImageService
    {
        public BitmapSource GenerateBarcodeImage(string barcode);
    }
}
