using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using QRCodes.Data;
using QRCodes.Models;
using QRCodes.Views;
using System.Collections.ObjectModel;

namespace QRCodes.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly DatabaseContext _databaseContext;

    [ObservableProperty]
    private ObservableCollection<QrCode> _qrCodes = [];

    [ObservableProperty]
    private QrCode? _selectedQrCode = null;

    public ListViewModel(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task LoadQrCodesAsync()
    {
        var qrCodes = await _databaseContext.QrCodes.ToListAsync();
        QrCodes = qrCodes.ToObservableCollection();
    }

    [RelayCommand]
    private async Task ViewQrCodeAsync()
    {
        if (SelectedQrCode is null)
        {
            return;
        }

        var navParameter = new ShellNavigationQueryParameters
        {
            { nameof(QrCode), SelectedQrCode }
        };

        await Shell.Current.GoToAsync($"/{nameof(QrCodeDetailsPage)}", true, navParameter);
        SelectedQrCode = null;
    }
}
