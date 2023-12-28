using Android.Content;

namespace QRCodes;

public static class SavePictureService
{
    public static bool SavePicture(byte[] arr, string imageName, out string? path)
    {
        try
        {
            var context = Platform.CurrentActivity;
            Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            path = Path.Combine(storagePath.ToString(), imageName);
            File.WriteAllBytes(path, arr);
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(path)));
            context.SendBroadcast(mediaScanIntent);
            return true;
        }
        catch (Exception)
        {
            path = null;
            return false;
        }
    }
}
