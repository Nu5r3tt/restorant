using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace RestaurantOrdering.Services
{
    public interface IQrCodeService
    {
        byte[] GenerateQrCode(string text);
        string GenerateQrCodeAsBase64(string text);
        byte[] GenerateTableQrCode(int tableNumber, string baseUrl);
        string GenerateTableQrCodeAsBase64(int tableNumber, string baseUrl);
    }

    public class QrCodeService : IQrCodeService
    {
        public byte[] GenerateQrCode(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        public string GenerateQrCodeAsBase64(string text)
        {
            var qrCodeBytes = GenerateQrCode(text);
            return Convert.ToBase64String(qrCodeBytes);
        }

        public byte[] GenerateTableQrCode(int tableNumber, string baseUrl)
        {
            var url = $"{baseUrl}/menu?table={tableNumber}";
            return GenerateQrCode(url);
        }

        public string GenerateTableQrCodeAsBase64(int tableNumber, string baseUrl)
        {
            var qrCodeBytes = GenerateTableQrCode(tableNumber, baseUrl);
            return Convert.ToBase64String(qrCodeBytes);
        }
    }
}
