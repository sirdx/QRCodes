using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using QRCodes.Data;
using QRCodes.Models;
using System.Collections.ObjectModel;

namespace QRCodes.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly DatabaseContext _databaseContext;

    [ObservableProperty]
    private ObservableCollection<QrCode> _qrCodes = [];

    public ListViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task LoadQrCodesAsync()
    {
        var qrCodes = await _databaseContext.QrCodes.ToListAsync();
        QrCodes = qrCodes.ToObservableCollection();
    }
}