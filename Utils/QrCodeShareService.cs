using QRCoder;
using QRCodes.Models;

namespace QRCodes.Utils;

public static class QrCodeShareService
{
    private static byte[] GenerateQrCodeImageBytes(QrCode qrCode)
    {
        var qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCode.Content, QRCodeGenerator.ECCLevel.L);

        var png = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = png.GetGraphic(20);
        var qrImageSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        return qrCodeBytes;
    }

    public static string? SaveToGallery(QrCode qrCode)
    {
        var qrCodeBytes = GenerateQrCodeImageBytes(qrCode);
        string fn = $"{Guid.NewGuid()}.png";

#if ANDROID
        SavePictureService.SavePicture(qrCodeBytes, fn, out string? path);
        return path;
#endif
    }

    public static async Task PrintAsync(QrCode qrCode)
    {
        var qrCodeBytes = GenerateQrCodeImageBytes(qrCode);
        var path = FileSystem.Current.CacheDirectory;
        string fn = $"{Guid.NewGuid()}.png";
        var fullPath = Path.Combine(path, fn);
        await File.WriteAllBytesAsync(fullPath, qrCodeBytes);
        string file = Path.Combine(FileSystem.CacheDirectory, fn);

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Generated QR Code",
            File = new ShareFile(file)
        });
    }
}