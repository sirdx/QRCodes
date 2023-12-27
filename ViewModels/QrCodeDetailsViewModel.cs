using CommunityToolkit.Mvvm.ComponentModel;
using QRCodes.Data;
using QRCodes.Models;

namespace QRCodes.ViewModels;

public partial class QrCodeDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly DatabaseContext _databaseContext;

    [ObservableProperty]
    private QrCode? _qrCode = null;

    public QrCodeDetailsViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        QrCode = query[nameof(QrCode)] as QrCode;
    }
}