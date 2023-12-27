using QRCodes.Views;

namespace QRCodes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(QrCodeDetailsPage), typeof(QrCodeDetailsPage));
    }
}